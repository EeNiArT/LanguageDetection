using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DialogueMaster.Babel;
using System.Globalization;

namespace WinSample
{
    public partial class DemoForm : Form
    {
        IBabelModel m_Model;

        public DemoForm()
        {
            // System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            InitializeComponent();
            this.m_Model = BabelModel._AllModel;

            foreach (String lang in m_Model.Keys)
            {
                object val = lang;
                try
                {
                    val = GetLanguageCulture(lang);
                }
                catch { };
                this.cbLangCompare.Items.Add(val);
                this.cbTables.Items.Add(val);
            }
            this.cbTables.SelectedIndex = 0;

        }

        public static CultureInfo GetLanguageCulture(string lang)
        {
            object val = lang;
            try
            {
                return CultureInfo.GetCultureInfoByIetfLanguageTag(lang);
            }
            catch
            {
                foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
                {
                    if (ci.TwoLetterISOLanguageName == lang)
                    {
                        return ci;
                    }
                }
            }
            throw new ArgumentException("lang", "Unknwon language");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void cbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lang = ((CultureInfo)cbTables.SelectedItem).TwoLetterISOLanguageName;
            ITokenTable table = this.m_Model[lang] as TokenTable;

            int maxRank = 0;
            int maxWordRank = 0;
            int maxCharRank = 0;

            lvNGrams.BeginUpdate();
            lvNGrams.Items.Clear();
            lvWords.BeginUpdate();
            lvWords.Items.Clear();
            listView3.BeginUpdate();
            listView3.Items.Clear();
            lvCharset.BeginUpdate();
            lvCharset.Items.Clear();

            foreach (string key in table.Keys)
            {
                ITokenStats stats = table[key];
                maxRank = System.Math.Max(maxRank, stats.Rank);
                ListViewItem item = new ListViewItem(stats.Position.ToString());
                item.SubItems.Add(stats.Token);
                item.SubItems.Add(stats.Rank.ToString());
                item.SubItems.Add(stats.Occurences.ToString());
                StringBuilder sbHex = new StringBuilder();
                foreach (char c in stats.Token)
                {
                    int Val = (int)c;
                    if (c > 255)
                    {
                        sbHex.AppendFormat("{0}{1} ", Convert.ToString((byte)((c & 0xff00) >> 8), 16), Convert.ToString((byte)c, 16));
                    }
                    else
                    {
                        sbHex.AppendFormat("{0} ", Convert.ToString((byte)c, 16));

                    }
                }
                item.SubItems.Add(sbHex.ToString().Trim());
                lvNGrams.Items.Add(item);
            }

            ITokenTable wordTable = table.WordTable;
            foreach (string key in wordTable.Keys)
            {
                ITokenStats stats = wordTable[key];
                maxWordRank = System.Math.Max(maxWordRank, stats.Rank);
                ListViewItem item = new ListViewItem(stats.Position.ToString());
                item.SubItems.Add(stats.Token);
                item.SubItems.Add(stats.Rank.ToString());
                item.SubItems.Add(stats.Occurences.ToString());
                lvWords.Items.Add(item);
            }

            ITokenTable charsetTable = table.CharsetTable;
            foreach (string key in charsetTable.Keys)
            {
                ITokenStats stats = charsetTable[key];
                maxCharRank = System.Math.Max(maxCharRank, stats.Rank);
                ListViewItem item = new ListViewItem(stats.Position.ToString());
                item.SubItems.Add(stats.Token);
                item.SubItems.Add(stats.Rank.ToString());
                item.SubItems.Add(stats.Occurences.ToString());
                lvCharset.Items.Add(item);
            }

            this.tbTokenRanks.Text = maxRank.ToString();
            this.tbWordRanks.Text = maxWordRank.ToString();
            this.tbCharRanks.Text = maxCharRank.ToString();

            double maxScore = 0;
            List<TableVoter> tables = new List<TableVoter>();
            foreach (string key in this.m_Model.Keys)
            {
                    TableVoter tableVoter = new TableVoter(key, table.ComparisonScore(this.m_Model[key], 0));
                    maxScore = Math.Max(maxScore, tableVoter.Score );
                    tables.Add(tableVoter);
            }
            tables.Sort();

            foreach(TableVoter voter in tables)
            {
                if (voter.Language != lang)
                {
                    voter.Score /= maxScore;
               //     voter.Score = 100 - voter.Score *200;
                    voter.Score = Math.Max(0,100 - (voter.Score *150));

                    // from 90 on there is not enough similarity to be wrongly detected 
                    if (voter.Score > 90)
                        continue;
                    ListViewItem item = new ListViewItem(voter.Language);

                    item.SubItems.Add(GetLanguageCulture(voter.Language).DisplayName.ToString());
                    item.SubItems.Add(voter.Score.ToString("0.00"));
                    listView3.Items.Add(item);
                }
              
            }


            lvNGrams.EndUpdate();
            lvWords.EndUpdate();
            listView3.EndUpdate();
            lvCharset.EndUpdate();


        }



        internal sealed class TableVoter : IComparable
        {
            private string m_Language;
            private double m_Score = 1;
            public TableVoter(string category)
            {
                this.m_Language = category;
            }
            public TableVoter(string category, double score)
            {
                this.m_Language = category;
                this.m_Score = score;
            }

           

            public double Score
            {
                get { return this.m_Score; }
                set { this.m_Score = value; }
            }
            public string Language
            {
                get { return this.m_Language; }
            }

            public override string ToString()
            {
                return this.m_Language + ":" + this.m_Score.ToString();
            }


            #region IComparable Members

            public int CompareTo(object obj)
            {
                if (obj is TableVoter)
                {
                    return -1 * this.m_Score.CompareTo(((TableVoter)obj).Score);
                }
                return 0;
            }

            #endregion

        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.cbLangCompare.SelectedIndex = -1;

            DialogueMaster.Classification.ICategoryList result =this.m_Model.ClassifyText(this.tbSource.Text);
            this.tbResult.Text = result.ToString();
            if (result.Count > 0)
            {
                this.cbLangCompare.SelectedItem = GetLanguageCulture(result[0].Name);
            }

        }

        private void cbLangCompare_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lvAnalyzeCharsetResult.BeginUpdate();
            this.lvAnalyzeCharsetResult.Items.Clear();
            this.lvAnalyzeNGramsResult.BeginUpdate();
            this.lvAnalyzeNGramsResult.Items.Clear();
            this.lvAnalyzeWordResult.BeginUpdate();
            this.lvAnalyzeWordResult.Items.Clear();

            if (this.cbLangCompare.SelectedIndex != -1)
            {
                TokenTable testTable = new TokenTable(this.tbSource.Text);
                ITokenTable compareTable = this.m_Model[((CultureInfo)cbLangCompare.SelectedItem).TwoLetterISOLanguageName];
                    
                List<TokenStats> scores = new List<TokenStats>();
                int score = compareTable.Count * testTable.Count;

                foreach (ITokenStats test in testTable.Values)
                {
                    TokenStats newScore = new TokenStats(test.Token);

                    int otherRank = compareTable.RankOf(test.Token);
                    if (otherRank == -1)
                    {
                        newScore.Occurences = compareTable.Count;
                        score -= compareTable.Count;
                    }
                    else
                    {
                        int val =  System.Math.Abs(test.Rank - otherRank) ;
                        newScore.Occurences = val;
                        // abuse the ran field to store the occurences...
                        newScore.Rank = test.Occurences;
                        score -= val;
                        scores.Add(newScore);
                    }
                }
                tbSumTokens.Text = score.ToString();

                scores.Sort();
                for(int i=scores.Count-1;i>-1;i--)
                {
                    TokenStats stats = scores[i];
                    ListViewItem item = new ListViewItem(stats.Token);
                    item.SubItems.Add(stats.Occurences.ToString());
                    item.SubItems.Add(stats.Rank.ToString());
                    lvAnalyzeNGramsResult.Items.Add(item);
                }





                scores = new List<TokenStats>();
                score = compareTable.WordTable.Count * testTable.WordTable.Count;

                foreach (ITokenStats test in testTable.WordTable.Values)
                {
                    TokenStats newScore = new TokenStats(test.Token);
                    
                    int otherRank = compareTable.WordTable.RankOf(test.Token);
                    if (otherRank == -1)
                    {
                        newScore.Occurences = compareTable.Ranks;
                    }
                    else
                    {
                        int val = System.Math.Abs(test.Rank - otherRank);
                        newScore.Occurences = val;
                        newScore.Rank = test.Occurences;
                        score -= val;
                        scores.Add(newScore);
                    }
                }
                int hits = 0;
                Double wsScore = compareTable.WordComparisonScore(testTable, 0, ref hits);

                tbSumWords.Text = wsScore.ToString("0.00")+" ("+hits+")";

                scores.Sort();
                for (int i = scores.Count - 1; i > -1; i--)
                {
                    TokenStats stats = scores[i];
                    ListViewItem item = new ListViewItem(stats.Token);
                    item.SubItems.Add(stats.Occurences.ToString());
                    item.SubItems.Add(stats.Rank.ToString());
                    lvAnalyzeWordResult.Items.Add(item);
                }






                scores = new List<TokenStats>();
                score = compareTable.CharsetTable.Count * testTable.CharsetTable.Count;

                foreach (ITokenStats test in testTable.CharsetTable.Values)
                {
                    TokenStats newScore = new TokenStats(test.Token);

                    int otherRank = compareTable.CharsetTable.RankOf(test.Token);
                    if (otherRank == -1)
                    {
                        newScore.Occurences = compareTable.Ranks;
                    }
                    else
                    {
                        int val = System.Math.Abs(test.Rank - otherRank);
                        newScore.Occurences = val;
                        newScore.Rank = test.Occurences;
                        score -= val;
                        scores.Add(newScore);
                    }
                }
                textBox1.Text = score.ToString();

                scores.Sort();
                for (int i = scores.Count - 1; i > -1; i--)
                {
                    TokenStats stats = scores[i];
                    ListViewItem item = new ListViewItem(stats.Token);
                    item.SubItems.Add(stats.Occurences.ToString());
                    item.SubItems.Add(stats.Rank.ToString());
                    lvAnalyzeCharsetResult.Items.Add(item);
                }

            }

            this.lvAnalyzeNGramsResult.EndUpdate();
            this.lvAnalyzeWordResult.EndUpdate();
            this.lvAnalyzeCharsetResult.EndUpdate();


        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvCharset_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbTokenRanks_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbSumWords_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbSumTokens_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
