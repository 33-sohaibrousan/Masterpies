using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Masterpies.Models;

namespace Masterpies.Controllers
{
    public class AppointmentsController : Controller
    {
        private MasterPieseEntities2 db = new MasterPieseEntities2();

        // GET: Appointments
        public ActionResult Index()
        {
            var appointments = db.Appointments.Include(a => a.AspNetUser).Include(a => a.Device).Include(a => a.TimeSlot);
            return View(appointments.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.aspuserid = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.DeviceID = new SelectList(db.Devices, "DeviceID", "DeviceName");
            ViewBag.TimeSlotID = new SelectList(db.TimeSlots, "TimeSlotID", "TimeSlotID");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentID,FirstName,LastName,patientAge,patientgender,city,TimeSlotID,DeviceID,AppointmentDate,aspuserid")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.aspuserid = new SelectList(db.AspNetUsers, "Id", "Email", appointment.aspuserid);
            ViewBag.DeviceID = new SelectList(db.Devices, "DeviceID", "DeviceName", appointment.DeviceID);
            ViewBag.TimeSlotID = new SelectList(db.TimeSlots, "TimeSlotID", "TimeSlotID", appointment.TimeSlotID);
            return View(appointment);
        }

        //---=-===============================================================
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public PartialViewResult _Apprtointment([Bind(Include = "AppointmentID,FirstName,LastName,patientAge,patientgender,city,TimeSlotID,DeviceID,AppointmentDate,aspuserid")]Appointment appointment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Appointments.Add(appointment);
        //        db.SaveChanges();
        //        //return RedirectToAction("Index");
        //    }

        //    //ViewBag.aspuserid = new SelectList(db.AspNetUsers, "Id", "Email", appointment.aspuserid);
        //    //ViewBag.DeviceID = new SelectList(db.Devices, "DeviceID", "DeviceName", appointment.DeviceID);
        //    //ViewBag.TimeSlotID = new SelectList(db.TimeSlots, "TimeSlotID", "TimeSlotID", appointment.TimeSlotID);
        //    return PartialView(appointment);
        //}

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.aspuserid = new SelectList(db.AspNetUsers, "Id", "Email", appointment.aspuserid);
            ViewBag.DeviceID = new SelectList(db.Devices, "DeviceID", "DeviceName", appointment.DeviceID);
            ViewBag.TimeSlotID = new SelectList(db.TimeSlots, "TimeSlotID", "TimeSlotID", appointment.TimeSlotID);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentID,FirstName,LastName,patientAge,patientgender,city,TimeSlotID,DeviceID,AppointmentDate,aspuserid")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.aspuserid = new SelectList(db.AspNetUsers, "Id", "Email", appointment.aspuserid);
            ViewBag.DeviceID = new SelectList(db.Devices, "DeviceID", "DeviceName", appointment.DeviceID);
            ViewBag.TimeSlotID = new SelectList(db.TimeSlots, "TimeSlotID", "TimeSlotID", appointment.TimeSlotID);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
