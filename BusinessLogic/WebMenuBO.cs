using DataAccess;
using DataModel;
using DataModel.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class WebMenuBO
    {
        DbContextOptions<OshanakCommonContext> _comOptions;
        public static readonly WebMenuBO Instance = new WebMenuBO(new DbContextOptions<OshanakCommonContext>());
        public WebMenuBO(DbContextOptions<OshanakCommonContext> comOptions)
        {
            _comOptions = comOptions;
        }

        public IQueryable<int> GetUserWorkGroupIds(Guid userId)
        {
            using (OshanakCommonContext db = new OshanakCommonContext(_comOptions))
            {
                var q = from gu in db.Web_WorkgroupUsers
                        where gu.UserId == userId && gu.IsActive == true
                        select gu.WorkGroupId;
                return q;
            }
        }

        public List<ViewMenuItemDS> GetMenuItems(int? parentMenuItem, Guid userId)
        {
            try
            {
                if (!userId.Equals(Guid.Empty))
                {
                    using (OshanakCommonContext db = new OshanakCommonContext(_comOptions))
                    {
                        var operations = (from op in db.Web_Operations where op.ApplicationId == 300 select op);

                        //var userId = this.operatorInfo.UserId;
                        var userGroupIds = from gu in db.Web_WorkgroupUsers
                                           where gu.UserId == userId && gu.IsActive == true
                                           select gu.WorkGroupId;
                        //var possibleApplications = facade.WebEnterpriseBO.GetPossibleApplicationsDependOfHostingType(hostingTypeId);


                        var userPossibleOperations = from ug in db.Web_WorkgroupOperations
                                                     join op in operations on ug.OperationId equals op.OperationId
                                                     where userGroupIds.Contains(ug.WorkGroupId) && ug.IsActive == true && op.ApplicationId == 300
                                                     select ug.OperationId;
                        var q = from mi in db.Web_MenuItems
                                where mi.IsActive == true
                                select mi;
                        if (parentMenuItem != null)
                        {
                            q = q.Where(x => x.ParentMenuItemId == parentMenuItem);
                        }
                        else
                        {
                            q = q.Where(x => x.ParentMenuItemId == null);
                        }
                        //  if (this.operatorInfo.UserName == "zfallahi") { q = q.Where(x => x.ParentMenuItemId == 106) }

                        //var operations = (from op in db.Web_Operations select op).ToList();
                        //var menuitems = q.ToList();

                        var q2 = from mi in q
                                 join op in operations on mi.OperationId equals op.OperationId
                                 where userPossibleOperations.Contains(op.OperationId)
                                 orderby mi.OrderNo
                                 select new ViewMenuItemDS
                                 {
                                     //Action = op != null ? op.ActionName : "",
                                     //Controller = op != null ? op.Controller : "",
                                     //Area = op != null ? op.Area : "",
                                     OperationId = op != null ? op.OperationId : 0,
                                     MenuItemID = mi.MenuItemId,
                                     IsLeaf = (mi.IsLeaf == null ? false : (bool)mi.IsLeaf),
                                     Title = mi.Title ?? "-",
                                     Url = (mi.Url == null ? (op.Area + "/" + op.Controller + "/" + op.ActionName) : mi.Url),
                                 };
                        return q2.ToList();
                    }
                }
                else
                {
                    return new List<ViewMenuItemDS>();
                }
            }
            catch (Exception ex)
            {
                return new List<ViewMenuItemDS>();
            }
        }

        public ViewMenuItemDS GetViewMenuItemDS(int menuItemId)
        {
            using (OshanakCommonContext db = new OshanakCommonContext(_comOptions))
            {
                var q2 = from mi in db.Web_MenuItems
                         join op in db.Web_Operations on mi.OperationId equals op.OperationId
                         /*from op in
                             (from op in db.Web_Operations where op.OperationId == mi.OperationId && op.ApplicationId == 300 select op).DefaultIfEmpty()*/
                         where mi.MenuItemId == menuItemId && op.ApplicationId == 300
                         orderby mi.OrderNo
                         select new ViewMenuItemDS
                         {
                             //Action = op != null ? op.ActionName : "",
                             //Controller = op != null ? op.Controller : "",
                             //Area = op != null ? op.Area : "",
                             OperationId = op != null ? op.OperationId : 0,
                             MenuItemID = mi.MenuItemId,
                             IsLeaf = mi.IsLeaf ?? false,
                             Title = mi.Title ?? "-",
                             Url = mi.Url == "" ? (op.Area + "/" + op.Controller + "/" + op.ActionName) : mi.Url,
                         };

                return q2.FirstOrDefault();
            }

        }

        public ViewMenuDS GenerateMenuInfo(int? menuItemId, int selectedMenuItemId, Guid userId)
        {
            //int hostingTypeId = facade.WebEnterpriseBO.GetHostingTypeId();
            ViewMenuDS result = new ViewMenuDS();
            menuItemId = menuItemId != 0 ? menuItemId : null;
            var rootMenuItems = GetMenuItems(menuItemId, userId);
            List<ViewMenuItemDS> mustBeRemove = new List<ViewMenuItemDS>();
            foreach (var mi in rootMenuItems)
            {
                bool isInsideChailds = LoadMenuItemChildrens(mi, selectedMenuItemId, userId);
                if (mi.OperationId == 0 && string.IsNullOrEmpty(mi.Url) && mi.MenuItems.Count < 1)
                {
                    mustBeRemove.Add(mi);
                }
                else
                {
                    if (isInsideChailds)
                    {
                        mi.IsHilighted = true;
                    }
                    if (mi.MenuItemID == selectedMenuItemId)
                    {
                        mi.IsSelected = true;
                        mi.IsHilighted = true;

                    }
                }
            }
            foreach (var mr in mustBeRemove)
            {
                rootMenuItems.Remove(mr);
            }

            if (menuItemId != null && menuItemId != 0)
            {
                ViewMenuItemDS home = GetViewMenuItemDS(9007039);
                rootMenuItems.Add(home);
            }
            result.MenuItems = rootMenuItems;
            return result;
        }

        private bool LoadMenuItemChildrens(ViewMenuItemDS mi, int selectedMenuItemId, Guid userId)
        {
            bool result = false;
            if (!mi.IsLeaf)
            {
                mi.MenuItems = GetMenuItems(mi.MenuItemID, userId);
                List<ViewMenuItemDS> mustBeRemove = new List<ViewMenuItemDS>();
                foreach (var cmi in mi.MenuItems)
                {
                    var isInsideChailds = LoadMenuItemChildrens(cmi, selectedMenuItemId, userId);

                    if ((cmi.OperationId == 0 && string.IsNullOrEmpty(cmi.Url) && cmi.MenuItems.Count < 1))
                    {
                        mustBeRemove.Add(cmi);
                    }
                    else
                    {
                        if (isInsideChailds)
                        {
                            cmi.IsHilighted = true;
                            result = true;
                        }
                        if (cmi.MenuItemID == selectedMenuItemId)
                        {
                            cmi.IsSelected = true;
                            cmi.IsHilighted = true;
                            result = true;
                        }
                    }
                }
                foreach (var mr in mustBeRemove)
                {
                    mi.MenuItems.Remove(mr);
                }
            }
            return result;
        }
    }
}
