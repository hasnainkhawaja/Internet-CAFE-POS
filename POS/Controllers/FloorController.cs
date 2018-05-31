using POS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Controllers
{
    public class FloorController : Controller
    {

        POSEntity.onlineinternetposEntities db = new POSEntity.onlineinternetposEntities();

        public JsonResult LoadDataForFloorsDataTable(Int64 storeid)
        {
            try
            {

                IEnumerable<POSEntity.SelectFloors_Result> FloorsList = SelectFloorsList(storeid).ToList();


                int recordsTotal = 0;
                

                recordsTotal = FloorsList.Count();
                var data = FloorsList.ToList();
                return Json(new { Result = "OK", recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<POSEntity.SelectFloors_Result> SelectFloorsList(Int64 storeid)
        {
            return db.SelectFloors(ClientSession.CompanyID,storeid).ToList();
        }

    }
}
