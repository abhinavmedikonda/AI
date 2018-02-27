using ActAI.DataRepository;
using ActAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ActAI.DataRepository.Helper;

namespace ActAI.Controllers
{
    public class ConfigureController : Controller
    {
        //private List<Theme> themes = new List<Theme> { new Theme() { ID = 1, Name = "XBox"
        //        , Groups = new List<Group> { new Group() { ID = 1, Name = "Fifa"
        //            , Phrases = new List<Phrase> { new Phrase() { ID = 1, Name = "SQL13892" }
        //            , new Phrase() { ID = 2, Name = "SQL53473" }}}
        //        , new Group { ID = 2, Name = "Tekken"
        //            , Phrases = new List<Phrase> { new Phrase() { ID = 3, Name = "DotNet48392" }
        //            , new Phrase() { ID = 4, Name = "SQL53473" }}}}}
        //    , new Theme() { ID = 2, Name = "Surface"
        //        , Groups = new List<Group> { new Group() { ID = 3, Name = "Pro"
        //            , Phrases = new List<Phrase> { new Phrase() { ID = 5, Name = "SQL13892" }
        //            , new Phrase() { ID = 6, Name = "DotNet48392" }}}
        //        , new Group { ID = 4, Name = "Book"
        //            , Phrases = new List<Phrase> { new Phrase() { ID = 7, Name = "SQL13892" }
        //            , new Phrase() { ID = 8, Name = "SQL53473" }}}}}
        //    , new Theme(){ID = 3, Name = "Windows", Groups = new List<Group>() } };

        // GET: Configure
        public ActionResult Configure()
        {
            ConfigureVM configureVM = new ConfigureVM();
            TrendAnalysisThemeRepository trendRepository = new TrendAnalysisThemeRepository();
            OrganisationRepository organisationRepository = new OrganisationRepository();
            SubOrganisationRepository subOrganisationRepository = new SubOrganisationRepository();
            ApplicationRepository applicationRepository = new ApplicationRepository();
            ThemeRepository themeRepository = new ThemeRepository();

            configureVM.Organisations = new List<Organisation>() { new Organisation() { Name = "" } };
            configureVM.Organisations.AddRange(organisationRepository.Get());
            configureVM.SubOrganisations = new List<SubOrganisation>() { new SubOrganisation() { Name = "" } };
            configureVM.SubOrganisations.AddRange(subOrganisationRepository.Get());
            configureVM.Applications = new List<Application>() { new Application() { Name = "" } };
            configureVM.Applications.AddRange(applicationRepository.Get());

            configureVM.Themes = themeRepository.DropDownSelect(0, 0, 0);

            return View(configureVM);
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

        public ActionResult DropDownSelect(string organisation, string subOrganisation, string application)
        {
            ConfigureVM configureVM = new ConfigureVM();
            TrendAnalysisThemeRepository trendRepository = new TrendAnalysisThemeRepository();
            OrganisationRepository organisationRepository = new OrganisationRepository();
            SubOrganisationRepository subOrganisationRepository = new SubOrganisationRepository();
            ApplicationRepository applicationRepository = new ApplicationRepository();
            ThemeRepository themeRepository = new ThemeRepository();

            configureVM.Organisations = new List<Organisation>() { new Organisation() { Name = "" } };
            configureVM.Organisations.AddRange(organisationRepository.Get());
            configureVM.SubOrganisations = new List<SubOrganisation>() { new SubOrganisation() { Name = "" } };
            configureVM.SubOrganisations.AddRange(subOrganisationRepository.Get());
            configureVM.Applications = new List<Application>() { new Application() { Name = "" } };
            configureVM.Applications.AddRange(applicationRepository.Get());

            configureVM.Themes = themeRepository.DropDownSelect(Convert.ToInt32(organisation),
                Convert.ToInt32(subOrganisation), Convert.ToInt32(application));

            return View("Configure", configureVM);
        }

        [HttpGet]
        public PartialViewResult _AddTheme()
        {
            return PartialView(new Theme());
        }

        [HttpGet]
        public PartialViewResult _AddGroup()
        {
            return PartialView(new Group());
        }

        [HttpGet]
        public PartialViewResult _AddPhrase()
        {
            return PartialView(new Phrase());
        }

        [HttpPost]
        public JsonResult AddTheme(Theme theme, string organisationID, string subOrganisationID, string applicationID)
        {
            ThemeRepository themeRepository = new ThemeRepository();
            int id = themeRepository.Insert(theme);

            Mapping mapping = new Mapping()
            {
                OrganisationID = organisationID == "0" ? null : (int?)Convert.ToInt32(organisationID),
                SubOrganisationID = subOrganisationID == "0" ? null : (int?)Convert.ToInt32(subOrganisationID),
                ApplicationID = applicationID == "0" ? null : (int?)Convert.ToInt32(applicationID),
                ThemeID = id
            };
            MappingRepository mappingRepository = new MappingRepository();
            mappingRepository.Insert(mapping);

            return Json(new { id = id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddGroup(Group group)
        {
            GroupRepository groupRepository = new GroupRepository();
            int id = groupRepository.Insert(group);

            return Json(new { id = id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddPhrase(Phrase phrase)
        {
            PhraseRepository phraseRepository = new PhraseRepository();
            int id = phraseRepository.Insert(phrase);

            return Json(new { id = id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool MovePhrase(int groupID, int phraseID)
        {
            PhraseRepository phraseRepository = new PhraseRepository();
            Phrase phrase = phraseRepository.Get(phraseID);
            phrase.GroupID = groupID;
            phraseRepository.Update(phrase);

            return true;
        }

        [HttpPost]
        public bool MoveGroup(int themeID, int groupID)
        {
            GroupRepository groupRepository = new GroupRepository();
            Group group = groupRepository.Get(groupID);
            group.ThemeID = themeID;
            groupRepository.Update(group);

            return true;
        }

        [HttpPost]
        public ViewResult AddTrend(TrendAnalysisTheme trendAnalysisTheme)
        {
            ConfigureVM configureVM = new ConfigureVM();
            TrendAnalysisThemeRepository trendRepository = new TrendAnalysisThemeRepository();

            configureVM.Organisations = new List<Organisation>() { null };
            configureVM.Organisations.AddRange(trendRepository.Organisations());
            configureVM.SubOrganisations = new List<SubOrganisation>() { null };
            configureVM.SubOrganisations.AddRange(trendRepository.SubOrganisations());
            configureVM.Applications = new List<Application>() { null };
            configureVM.Applications.AddRange(trendRepository.Applications());

            TrendAnalysisThemeRepositoryHelper trendRepositoryHelper = new TrendAnalysisThemeRepositoryHelper();
            configureVM.Themes = trendRepositoryHelper.TableToThemeEntity(trendRepository.GetThemes(trendAnalysisTheme.assignment_group_parent_parent, trendAnalysisTheme.assignment_group_parent, trendAnalysisTheme.configuration_item_application));

            return View("Configure", configureVM);
        }
    }
}