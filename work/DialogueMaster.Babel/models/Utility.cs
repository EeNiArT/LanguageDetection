using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DialogueMaster.Babel.models
{
    public class Utility
    {
        IBabelModel m_Model;



        public string detecte(string texte)
        {


            string tea = texte;
            string baad = "";

            if (tea.Contains("2") || tea.Contains("3") || tea.Contains("4") || tea.Contains("5") || tea.Contains("7") || tea.Contains("9"))
            {
                baad = "Arabizi";
                //return;
            }


            //this.cbLangCompare.SelectedIndex = -1;

            DialogueMaster.Classification.ICategoryList result = this.m_Model.ClassifyText(tea);
            //this.tbResult.Text = result.ToString();
            if (result.Count > 0)
            {
                var infoooo = GetLanguageCulture(result[0].Name);
                //this.cbLangCompare.SelectedItem = GetLanguageCulture(result[0].Name);

                baad = infoooo.DisplayName;
            }


            return "";
        }


        #region supporting method

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

        #endregion

    }
}
