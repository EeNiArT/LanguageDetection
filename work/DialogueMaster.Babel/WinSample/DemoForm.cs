using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DialogueMaster.Babel;
using System.Globalization;
using System.Linq;

namespace WinSample
{
    public partial class DemoForm : Form
    {
        IBabelModel m_Model;

        public string x1 = "AA,aando,ach,achmn,achne,achno,Akhti,Akhtii,ana,andhom,asg,awdi,awdii,axmn,ay,ayi,b,bach,Bacha,Bachahal,Bachahl,Bagha,baghi,baki,bali,ban,bante,barit,basah,bax,bazaf,bch,bchhal,bchi,bchitaman,beghit,Beghite,Bgha,bghayt,Bghina,bghit,bghite,bghitha,Bghitt,Bghut,Bghyt,bgit,bgito,bihoum,bik,bin,binatkom,binatna,binek,bini,bit,Bixahla,biya,bjoj,bjouj,bla,brayt,brit,brite,bsah,btaman,bzaaf,bzaf,bzf,bzzf,ch,chi,chkon,chkoun,chmen,chmn,chno,hhnou,chof,lia,chorout,chrihaaa,ctro,daba,dak,dar,dar,darou,dayr,dayra,dayrin,db,dert,dial,dik,dima,din,dir,dirou,diyal,drb,dyal,dyalha,dyalhoum,dyalo,dyl,ee,ela,endi,f,fach,fal,fash,fax,fe,fel,fi,fia,fiha,fihom,fihoum,fik,fiki,fikoum,fina,fine,fiya,frzo,fx,fya,gh,Ghadi,ghda,ghi,ghir,go,l,h,had,hada,hadak,hadchi,hade,hadi,hado,hadou,hadxi,haja,hana,hanti,hat,hata,hatchi,heta,hia,hir,hit,hiya,hna,ho,hoa,houa,hta,hwa,hya,ido,ikon,il,ila,imta,ina,incha,inxa,iwa,iyah,ja,jawbo,jawboni,jawbt,jit,jwbo,kain,kam,kan,kanb4i,kanbda,kanbghi,kanhtajha,Kanmout,kant,kayen,kayena,kayenin,kayn,kayna,khas,khasni,khass,khdit,Khodi,khoti,khouti,khouya,khoya,khsni,khti,kif,kifach,kifax,kima,kin,kindir,kohom,kolchi,kolna,kolo,kolona,kon,kont,konx,koulna,kount,kter,l,lah,lah,lakom,leya,lgharad,lhal,li,lia,lih,liha,lihoum,lik,likom,lina,liya,lkhot,lok,lya,Lyom,Lyoma,m,M3ach,M3ax,machi,makaynch,malk,man,mane,mara,matfoutch,matx,maxi,mazal,mazyan,mcha,mchat,Mchhal,mchi,mchit,mchiw,memkin,men,men rda, mera, Mezyana, mhtj, mi, mjoud, mli, mlk, mlyon, mmkin, mn, momki, momkin, momkin l,monasib,monassib,n,na,naaraf,nacherii,naftah,nakhod,namchi,namchiw n, nari, narii, Nbaliw, nbdlha, Nchri, nchriha, ndir, ndiroha, nhar, nkhdem, nkhoud, nkono, nkri, nsawlak, nshofo, nshriha, nstafd, nta, ntia, Ntmana, Ntmnaw, ntolo, ntoma, ntouma, ntsnaw, ntya, nxri, o, o3andi, o3endi, obghit, obrit, ochi, ochno, ofih, Ofin, ola, omhtaja, ouach, plize, Q, ra, rah, raha, rahna, rahoum, rani, rer, rir, rizo, salam, salame, salamo, salm, sbah, sbh, shi, shnou, shnu, sift, sifto, sir, siri, siro, sirou, slam, Slm, tafasil, tajwboni, taman, tdir, techri, tfawed, tfo, tjrabha, tkon, tkoun, tmchi, toma, touma, Twkeel, twkl, twkli, w, wa, waa, waaa, waahda, wach, wache, wahda, wahed, wal, walou, wana, wash, wax, wch, wehda, wel, wela, welad, whta, wla, wlad, Wlahila, wld, Wlit, Wlit,, Wlito, Wlitoo, wno, wnou, wntoma, wordsletters, wslat, wslt, xhal, xi, xini, xiwhda, xkon, xkoon, xno, xnu, xofo, xokran, xokrn, xrit, xukran, ya, yak, yaka, yakou, yalah, yallah, yamken, yamkn, yarab, yarabi, yla, ymken, ymkn, ymta, yslah, zwin, zwina,";

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


            //strt
            this.cbLangCompare.Items.Add("Arabizi");
            //this.cbTables.SelectedIndex = 0;

            lvAnalyzeCharsetResult.Visible = false;//left
            lvAnalyzeWordResult.Visible = false;
            lvAnalyzeNGramsResult.Visible = false;
            textBox1.Visible = false;
            tbSumWords.Visible = false;
            tbSumTokens.Visible = false;
            tbResult.Visible = false;
            cbLangCompare.Visible = false;
            //label8.Text = "Type our language text here";

           

            List<string> entitylist = new List<string>();

            entitylist.Add("aa"); entitylist.Add("aando"); entitylist.Add("ach"); entitylist.Add("achmn"); entitylist.Add("achne");
            entitylist.Add("Akhti"); entitylist.Add("Akhtii"); entitylist.Add("ana"); entitylist.Add("andhom"); entitylist.Add("asg");
            entitylist.Add("awdi"); entitylist.Add("awdii"); entitylist.Add("axmn"); entitylist.Add("ay"); entitylist.Add("ayi");

            entitylist.Add(""); entitylist.Add(""); entitylist.Add(""); entitylist.Add(""); entitylist.Add("");
            entitylist.Add(""); entitylist.Add(""); entitylist.Add(""); entitylist.Add(""); entitylist.Add("");

            //var xlApp = new Microsoft.Office.Interop.Excel.Application();
            //var xlWorkBook = xlApp.Workbooks.Open(@"F:\work1\DialogueMaster.Babel\WinSample\file\entities.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            //var xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


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
                maxScore = Math.Max(maxScore, tableVoter.Score);
                tables.Add(tableVoter);
            }
            tables.Sort();

            foreach (TableVoter voter in tables)
            {
                if (voter.Language != lang)
                {
                    voter.Score /= maxScore;
                    //     voter.Score = 100 - voter.Score *200;
                    voter.Score = Math.Max(0, 100 - (voter.Score * 150));

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
            string tea = this.tbSource.Text;
            string obj2 = "";
            //Utility obj = new Utility();
            //var tyhv = obj.detecte(tea);
            //showresponce(tyhv);
            //return;

            //tea = "3afak bghit wahda f my is lak7al tkon fial rjal makraht tkon sport";
            //string text = "This is an example string and my data is here";
            //string data = getBetween(tea, "my", "is");
            if (tea.Contains("2") || tea.Contains("3") || tea.Contains("4") || tea.Contains("5") || tea.Contains("7") || tea.Contains("9"))
            {
                obj2 = "yes";
            }
            else
            {
                string[] string1 = x1.Split(',');
                string[] string2 = tea.Split(' ');
                string[] m = string1.Distinct().ToArray();
                string[] n = string2.Distinct().ToArray();
                string Test;
                var results = m.Intersect(n, StringComparer.OrdinalIgnoreCase);
                Test = String.Join(" ", results);
                //return Test;
                obj2 = Test;


                //    if (string tea = AA || aando || ach || achmn || achne || achno || Akhti || Akhtii || ana || andhom || asg || awdi || awdii || axmn || ay ||ayi || b || bach || Bacha || Bachahal || Bachahl || Bagha || baghi || baki || bali || ban || bante || barit || basah || bax || bazaf || bch || bchhal || bchi || bchitaman || beghit || Beghite || Bgha || bghayt 
                //                        || Bghina || bghit || bghite || bghith || a || Bghitt || Bghut || Bghyt || bgit || bgito || bihoum || bik || bin || binatkom || binatna || binek || bini || bit || Bixahla || biya || bjoj || bjouj || bla || brayt || brit || brite || bsah || btaman || bzaaf || bzaf || bzf || bzzf || ch || chi || chko || n || chkoun || chmen || chmn 
                //                        || chno || hhnou || chof || lia || chorout || chrihaaa || ctro || daba || dak || dar || dar || darou || dayr || dayra || dayrin || db || dert || dial || dik || dima || din || dir || dirou || diyal || drb || dyal || dyalha || dyalho ||um || dyalo || dyl || ee || ela || endi || f || fach || fal || fash ||fax || fe || fel || fi || fia 
                //                        || fiha || fihom || fihoum || fik || fiki || fikoum || fina || fine || fiya || frzo || fx || fya || gh || Ghadi || ghda || ghi || ghir || go || l || h || had || hada || hadak || ha || dchi || hade || hadi || hado || hadou || hadxi || haja || hana || hanti || hat || hata || hatchi || heta || hia || hir || hit || hiya || hna || ho 
                //                        || hoa || houa || hta || hwa || hya || ido||ikon || il || ila || imta || ina || incha || inxa || iwa || iyah || ja || jawbo ||jawboni || jawbt || jit || jwbo || kain || kam || kan || kanb4i || kanbda || kanbghi || kanhtajha || Kanmout || kant || kayen || kayena || kayenin || kayn || kayna || khas || khasni || khass || khdit || Khodi 
                //                        || khoti || khouti || khouya || kho || ya || khsni || khti || kif || kifach || kifax || kima || kin || kindir || kohom || kolchi || kolna || kolo || kolona || kon || kont || konx || koulna || kount || kter || l || lah || lah || lakom || leya || lgharad || lhal || li || lia|| lih || liha || lihoum ||lik || likom || lina || liya || lkhot 
                //                        || lok || lya || Lyom || Lyoma || m || M3ach || M3ax || machi || makaynch || malk || man || mane || mara || matfoutch || matx || maxi || mazal || mazyan || mcha || mchat || Mchhal || mchi || mchit || mchiw || memkin || men|| men rda || mera || Mezyana || mhtj || mi || mjoud || mli || mlk || mlyon || mmkin || mn || momki || momkin 
                //                        || momkin l || monasib || monassib || n || na || naaraf || nacherii || naftah || nakhod || namchi || namchiw ||n || nari || narii || Nbaliw || nbdlha || Nchri || nchriha || ndir || ndiroha || nhar || nkhdem || nkhoud || nkono || nkri || nsawlak || nshofo || nshriha || nstafd || nta || ntia || Ntmana || Ntmnaw || ntolo || ntoma 
                //                        || ntouma || ntsnaw || ntya || nxri || o || o3andi || o3endi || obghit || obrit || ochi || ochno || ofih || Ofin || ola || omhtaja || ouach || plize || Q || ra || rah || raha || rahna || rahoum || rani || rer || rir || rizo || salam || salame || salamo || salm || sbah || sbh || shi || shno || u || shnu || sift || sifto || sir || siri 
                //                        || siro || sirou || slam || Slm || tafasil || tajwboni || taman || tdir || techri || tfawed || tfo || tjrabha || tkon || tkoun || tmchi || toma || touma || Twkeel || twkl || twkli || w || wa || waa || waaa || waahda |||| wach || wache || wahda || wahed || wal || walou || wana || wash || wax || wch || wehda || wel || wela || welad 
                //                        || whta || wla || wlad || Wlahila || wld || Wlit || Wlit || Wlito || Wlitoo || wno || wnou || wntoma || wordsletters || wslat || wslt || xhal || xi || xini || xiwhda || xkon || xkoon || xno || xnu || xofo || xokran || xokrn || xrit || xukran || ya || yak || yaka||yakou || yalah || yallah || yamken || yamkn || yarab || yarabi || yla || ymken || ymkn || ymta || yslah || zwin || zwina)


            }
            

            if(obj2 != null && obj2 != "")
            {
                showresponce("Arabizi");
                return;
            }
                
            

            this.cbLangCompare.SelectedIndex = -1;

            DialogueMaster.Classification.ICategoryList result = this.m_Model.ClassifyText(this.tbSource.Text);
            this.tbResult.Text = result.ToString();
            if (result.Count > 0)
            {
                var infoooo = GetLanguageCulture(result[0].Name);
                this.cbLangCompare.SelectedItem = GetLanguageCulture(result[0].Name);

                showresponce(infoooo.DisplayName);
            }

        }

        public void showresponce(string value)
        {
            if (value == "Arabizi" || value == "Arabic" || value == "French" || value == "English" || value == "Spanish")
            {
                label9.Text = value;
            }
            else
            {
                label9.Text = "Other";//la langue détectée est
            }
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
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
                        int val = System.Math.Abs(test.Rank - otherRank);
                        newScore.Occurences = val;
                        // abuse the ran field to store the occurences...
                        newScore.Rank = test.Occurences;
                        score -= val;
                        scores.Add(newScore);
                    }
                }
                tbSumTokens.Text = score.ToString();

                scores.Sort();
                for (int i = scores.Count - 1; i > -1; i--)
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

                tbSumWords.Text = wsScore.ToString("0.00") + " (" + hits + ")";

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

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
