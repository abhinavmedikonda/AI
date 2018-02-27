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
    public class NewKeywordsController : Controller
    {
        // GET: NewWords
        public ActionResult NewKeywords()
        {
            NewKeywordsVM newKeywordsVM = new NewKeywordsVM();
            TrendAnalysisThemeRepository trendRepository = new TrendAnalysisThemeRepository();
            TrendAnalysisNewKeywordsListRepository newKeywordsRepository = new TrendAnalysisNewKeywordsListRepository();
            TrendAnalysisStopListRepository stopListRepository = new TrendAnalysisStopListRepository();

            newKeywordsVM.Organisations = new List<Organisation>() { null };
            newKeywordsVM.Organisations.AddRange(trendRepository.Organisations());
            newKeywordsVM.SubOrganisations = new List<SubOrganisation>() { null };
            newKeywordsVM.SubOrganisations.AddRange(trendRepository.SubOrganisations());
            newKeywordsVM.Applications = new List<Application>() { null };
            newKeywordsVM.Applications.AddRange(trendRepository.Applications());
            newKeywordsVM.NewKeywords = newKeywordsRepository.Get();
            newKeywordsVM.StopLists = stopListRepository.Get();

            TrendAnalysisThemeRepositoryHelper trendRepositoryHelper = new TrendAnalysisThemeRepositoryHelper();
            newKeywordsVM.Themes = trendRepositoryHelper.TableToThemeEntity(trendRepository.GetThemes(null, null, null));

            //newKeywordsVM.Organisations = new List<string>() { "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere", "asdferbtr", "rwtsfreear", "sdfsgere" };
            //newKeywordsVM.SubOrganisations = new List<string>() { "asdferbtr", "rwtsfreear", "sdfsgere" };
            //newKeywordsVM.Applications = new List<string>() { "asdferbtr", "rwtsfreear", "sdfsgere" };
            //newKeywordsVM.Themes = themes;

            return View(newKeywordsVM);
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
            NewKeywordsVM newKeywordsVM = new NewKeywordsVM();
            TrendAnalysisThemeRepository trendRepository = new TrendAnalysisThemeRepository();

            newKeywordsVM.Organisations = new List<Organisation>() { null };
            newKeywordsVM.Organisations.AddRange(trendRepository.Organisations());
            newKeywordsVM.SubOrganisations = new List<SubOrganisation>() { null };
            newKeywordsVM.SubOrganisations.AddRange(trendRepository.SubOrganisations());
            newKeywordsVM.Applications = new List<Application>() { null };
            newKeywordsVM.Applications.AddRange(trendRepository.Applications());

            TrendAnalysisThemeRepositoryHelper trendRepositoryHelper = new TrendAnalysisThemeRepositoryHelper();
            newKeywordsVM.Themes = trendRepositoryHelper.TableToThemeEntity(trendRepository.GetThemes(organisation, subOrganisation, application));

            return View("NewKeywords", newKeywordsVM);
        }

        [HttpPost]
        public PartialViewResult _AddTheme(TrendAnalysisTheme trendAnalysisTheme)
        {
            return PartialView(trendAnalysisTheme);
        }

        [HttpPost]
        public ViewResult AddTheme(TrendAnalysisTheme trendAnalysisTheme)
        {
            NewKeywordsVM newKeywordsVM = new NewKeywordsVM();
            TrendAnalysisThemeRepository trendRepository = new TrendAnalysisThemeRepository();

            TrendAnalysisThemeRepositoryHelper trendRepositoryHelper = new TrendAnalysisThemeRepositoryHelper();
            trendRepositoryHelper.InsertTheme(trendAnalysisTheme);

            newKeywordsVM.trendAnalysisTheme = trendAnalysisTheme;
            newKeywordsVM.Organisations = new List<Organisation>() { null };
            newKeywordsVM.Organisations.AddRange(trendRepository.Organisations());
            newKeywordsVM.SubOrganisations = new List<SubOrganisation>() { null };
            newKeywordsVM.SubOrganisations.AddRange(trendRepository.SubOrganisations());
            newKeywordsVM.Applications = new List<Application>() { null };
            newKeywordsVM.Applications.AddRange(trendRepository.Applications());

            newKeywordsVM.Themes = trendRepositoryHelper.TableToThemeEntity(trendRepository.GetThemes(
                trendAnalysisTheme.assignment_group_parent_parent, trendAnalysisTheme.assignment_group_parent,
                trendAnalysisTheme.configuration_item_application));

            return View("NewKeywords", newKeywordsVM);
        }

        [HttpPost]
        public void AddStopList(string stopList)
        {
            TrendAnalysisStopListRepository stopListRepository = new TrendAnalysisStopListRepository();
            stopListRepository.Insert(stopList);

            TrendAnalysisNewKeywordsListRepository newKeywordsRepository = new TrendAnalysisNewKeywordsListRepository();
            newKeywordsRepository.Delete(stopList);
        }

        [HttpPost]
        public void AddNewKeyword(string newKeyword)
        {
            TrendAnalysisNewKeywordsListRepository newKeywordsRepository = new TrendAnalysisNewKeywordsListRepository();
            newKeywordsRepository.Insert(newKeyword);

            TrendAnalysisStopListRepository stopListRepository = new TrendAnalysisStopListRepository();
            stopListRepository.Delete(newKeyword);
        }

        [HttpPost]
        public void MoveGroup(string theme, string group)
        {
            //TrendAnalysisThemeRepositoryHelper trendRepositoryHelper = new TrendAnalysisThemeRepositoryHelper();
            //trendRepositoryHelper.InsertKeyword(id, keyword);

            TrendAnalysisThemeRepository trendRepository = new TrendAnalysisThemeRepository();
            trendRepository.UpdateTheme(theme, group);
        }

        [HttpPost]
        public void MoveNewKeyword(int id, string newKeyword)
        {
            TrendAnalysisThemeRepositoryHelper trendRepositoryHelper = new TrendAnalysisThemeRepositoryHelper();
            trendRepositoryHelper.InsertKeyword(id, newKeyword);

            TrendAnalysisStopListRepository stopListRepository = new TrendAnalysisStopListRepository();
            stopListRepository.Delete(newKeyword);

            TrendAnalysisNewKeywordsListRepository newKeywordsRepository = new TrendAnalysisNewKeywordsListRepository();
            newKeywordsRepository.Delete(newKeyword);
        }

        [HttpPost]
        public ViewResult AddTrend(TrendAnalysisTheme trendAnalysisTheme)
        {
            NewKeywordsVM newKeywordsVM = new NewKeywordsVM();
            TrendAnalysisThemeRepository trendRepository = new TrendAnalysisThemeRepository();

            newKeywordsVM.Organisations = new List<Organisation>() { null };
            newKeywordsVM.Organisations.AddRange(trendRepository.Organisations());
            newKeywordsVM.SubOrganisations = new List<SubOrganisation>() { null };
            newKeywordsVM.SubOrganisations.AddRange(trendRepository.SubOrganisations());
            newKeywordsVM.Applications = new List<Application>() { null };
            newKeywordsVM.Applications.AddRange(trendRepository.Applications());

            TrendAnalysisThemeRepositoryHelper trendRepositoryHelper = new TrendAnalysisThemeRepositoryHelper();
            newKeywordsVM.Themes = trendRepositoryHelper.TableToThemeEntity(trendRepository.GetThemes(trendAnalysisTheme.assignment_group_parent_parent, trendAnalysisTheme.assignment_group_parent, trendAnalysisTheme.configuration_item_application));

            return View("NewKeywords", newKeywordsVM);
        }
    }
}