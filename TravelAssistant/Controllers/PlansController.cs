using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelAssistant.Models;
using TravelAssistant.ViewModel;

namespace TravelAssistant.Controllers
{
    [Authorize]
    public class PlansController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PlansController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            List<TravelPlan> currentUserPlans = new List<TravelPlan>();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var currentUserId = User.Identity.GetUserId();
                currentUserPlans = context.TravelPlan.Where(p => p.UserId == currentUserId).ToList();
            }

            return View(currentUserPlans);
        }

        public ActionResult AddNewPlan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewPlan(TravelPlan plan)
        {
            if (!ModelState.IsValid)
            {
                return View(plan);
            }

            plan.UserId = User.Identity.GetUserId();
            _context.TravelPlan.Add(plan);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = plan.TravelPlanId });
        }

        public ActionResult Details(int Id)
        {
            var model = new PlanViewModel();
            model.TravelPlanId = Id;
            var startingPosition = _context.Position
                .Where(p => p.TravelPlanId == Id && p.PositionType == PositionType.Start)
                .FirstOrDefault();

            model.StartingPointLat = startingPosition?.Latitude;
            model.StartingPointLon = startingPosition?.Longitude;

            var endingPosition = _context.Position
               .Where(p => p.TravelPlanId == Id && p.PositionType == PositionType.End)
               .FirstOrDefault();

            model.EndingPointLat = endingPosition?.Latitude;
            model.EndingPointLon = endingPosition?.Longitude;

            var waypoints = _context.Position
               .Where(p => p.TravelPlanId == Id && p.PositionType == PositionType.WayPoint)
               .ToList();

            model.WaypointsLat = new List<string>();
            model.WaypointsLon = new List<string>();
            foreach (var waypoint in waypoints)
            {
                model.WaypointsLat.Add(waypoint.Latitude);
                model.WaypointsLon.Add(waypoint.Longitude);
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var toDelete = _context.TravelPlan.Where(t => t.TravelPlanId == id).First();
            _context.TravelPlan.Remove(toDelete);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Details(PlanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var startingPosition = new Position();
            startingPosition.TravelPlanId = model.TravelPlanId;
            startingPosition.Latitude = model.StartingPointLat;
            startingPosition.Longitude = model.StartingPointLon;
            startingPosition.PositionType = PositionType.Start;

            var endingPosition = new Position();
            endingPosition.TravelPlanId = model.TravelPlanId;
            endingPosition.Latitude = model.EndingPointLat;
            endingPosition.Longitude = model.EndingPointLon;
            endingPosition.PositionType = PositionType.End;

            var wayPoints = new List<Position>();

            for (int i = 0; i < model.WaypointsLat?.Count; i++)
            {
                wayPoints.Add(
                    new Position
                    {
                        TravelPlanId = model.TravelPlanId,
                        Latitude = model.WaypointsLat[i],
                        Longitude = model.WaypointsLon[i],
                        PositionType = PositionType.WayPoint
                    });
            }

            _context.Position.Add(startingPosition);
            _context.Position.Add(endingPosition);
            if (wayPoints.Count > 0)
            {
                _context.Position.AddRange(wayPoints);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
            base.Dispose(disposing);
        }

    }
}