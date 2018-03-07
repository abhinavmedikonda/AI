using ActAI.DataRepository;
using ActAI.DataRepository.Helper;
using ActAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActAI.Web.Controllers
{
    public class NewPhrasesController : Controller
    {
        public ActionResult NewPhrases()
        {
            NewPhrasesVM newPhrasesVM = new NewPhrasesVM();
            OrganisationRepository organisationRepository = new OrganisationRepository();
            SubOrganisationRepository subOrganisationRepository = new SubOrganisationRepository();
            ApplicationRepository applicationRepository = new ApplicationRepository();
            ThemeRepository themeRepository = new ThemeRepository();
            NewPhraseRepository newPhraseRepository = new NewPhraseRepository();

            newPhrasesVM.Organisations = new List<Organisation>() { new Organisation() { Name = "" } };
            newPhrasesVM.Organisations.AddRange(organisationRepository.Get());
            newPhrasesVM.SubOrganisations = new List<SubOrganisation>() { new SubOrganisation() { Name = "" } };
            newPhrasesVM.SubOrganisations.AddRange(subOrganisationRepository.Get());
            newPhrasesVM.Applications = new List<Application>() { new Application() { Name = "" } };
            newPhrasesVM.Applications.AddRange(applicationRepository.Get());
            newPhrasesVM.Themes = themeRepository.DropDownSelect(0, 0, 0);
            newPhrasesVM.NewPhrases = newPhraseRepository.Get();

            return View(newPhrasesVM);
        }

        //public JsonResult GetGroups(string term)
        //{
        //    if (string.IsNullOrEmpty(term))
        //    {
        //        return Json(this.themes.Select(x => x.Name), JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(this.themes.Where(x => x.Name.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0).Select(x => x.Name), JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult DropDownSelect(int organisation, int subOrganisation, int application)
        {
            NewPhrasesVM newPhrasesVM = new NewPhrasesVM();
            OrganisationRepository organisationRepository = new OrganisationRepository();
            SubOrganisationRepository subOrganisationRepository = new SubOrganisationRepository();
            ApplicationRepository applicationRepository = new ApplicationRepository();
            ThemeRepository themeRepository = new ThemeRepository();
            NewPhraseRepository newPhraseRepository = new NewPhraseRepository();

            newPhrasesVM.Organisations = new List<Organisation>() { new Organisation() { Name = "" } };
            newPhrasesVM.Organisations.AddRange(organisationRepository.Get());
            newPhrasesVM.SubOrganisations = new List<SubOrganisation>() { new SubOrganisation() { Name = "" } };
            newPhrasesVM.SubOrganisations.AddRange(subOrganisationRepository.Get());
            newPhrasesVM.Applications = new List<Application>() { new Application() { Name = "" } };
            newPhrasesVM.Applications.AddRange(applicationRepository.Get());
            newPhrasesVM.Themes = themeRepository.DropDownSelect(organisation, subOrganisation, application);
            newPhrasesVM.NewPhrases = newPhraseRepository.Get();

            return View("NewPhrases", newPhrasesVM);
        }

        [HttpPost]
        public bool MoveToNewPhrases(int newPhraseID)
        {
            NewPhraseRepository newPhraseRepository = new NewPhraseRepository();
            NewPhrase newPhrase = newPhraseRepository.Get(newPhraseID);
            newPhrase.StopList = false;
            newPhraseRepository.Update(newPhrase);

            return true;
        }

        [HttpPost]
        public bool MoveToStopList(int newPhraseID)
        {
            NewPhraseRepository newPhraseRepository = new NewPhraseRepository();
            NewPhrase newPhrase = newPhraseRepository.Get(newPhraseID);
            newPhrase.StopList = true;
            newPhraseRepository.Update(newPhrase);

            return true;
        }

        [HttpPost]
        public bool MoveToGroup(int groupID, int newPhraseID)
        {
            NewPhraseRepository newPhraseRepository = new NewPhraseRepository();
            NewPhrase newPhrase = newPhraseRepository.Get(newPhraseID);

            PhraseRepository phraseRepository = new PhraseRepository();
            Phrase phrase = new Phrase();
            phrase.GroupID = groupID;
            phrase.Name = newPhrase.Name;
            phrase.Frequency = newPhrase.Frequency;

            phraseRepository.Insert(phrase);
            newPhraseRepository.Delete(newPhrase.ID);

            return true;
        }
    }
}