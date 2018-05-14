using DialogueMaster.Babel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LanguageDetectionApi.Controllers
{
    [RoutePrefix("api/Language")]
    public class LanguageController : ApiController
    {

        IBabelModel m_Model;

        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("Detect")]
        [HttpGet]
        public HttpResponseMessage GetDiscussionBords(string Mytext)
        {
            HttpResponseMessage message = null;
            try
            {
                string tea = Mytext;
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

                message = this.Request.CreateResponse(HttpStatusCode.OK, baad);
            }
            catch (Exception ex)
            {
                message = this.Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

            return message;
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
