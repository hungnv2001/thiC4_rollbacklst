using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hungnv_ph30098.Context;
using hungnv_ph30098.Models;
using hungnv_ph30098.Service.IService;
using hungnv_ph30098.Service;
using Newtonsoft.Json;

namespace hungnv_ph30098.Controllers
{
    public class CongViensController : Controller
    {
        private readonly ICVService dt;

        public CongViensController()
        {
            dt = new CongVienService();
        }

        // GET: CongViens
        public async Task<IActionResult> Index()
        {
            return dt.GetAll() != null ?
                        View(dt.GetAll()) :
                        Problem("Entity set 'MyContext.congViens'  is null.");
        }

        // GET: CongViens/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || dt.GetAll() == null)
            {
                return NotFound();
            }

            var congVien = dt.Find(id);
            if (congVien == null)
            {
                return NotFound();
            }

            return View(congVien);
        }

        // GET: CongViens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CongViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Type,DienTich,ThoiGianMo,Status")] CongVien congVien)
        {
            if (ModelState.IsValid)
            {
                dt.Create(congVien);
                return RedirectToAction(nameof(Index));
            }
            return View(congVien);
        }

        // GET: CongViens/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || dt.GetAll() == null)
            {
                return NotFound();
            }

            var congVien = dt.Find(id);
            if (congVien == null)
            {
                return NotFound();
            }
            return View(congVien);
        }

        // POST: CongViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Address,Type,DienTich,ThoiGianMo,Status")] CongVien congVien)
        {
            List<CongVien> lstCV = new List<CongVien>();
            //lấy ra list công viên đã sửa
            var oldjson = HttpContext.Session.GetString("lstjson");
            //kiểm tra có phần tử nào ko
            if (oldjson != null)
            {

                lstCV = JsonConvert.DeserializeObject<List<CongVien>>(oldjson);
                //kiểm tra cái mới sửa đã có chưa
                var jsonValid = lstCV.FirstOrDefault(p => p.Id == id);
                var newjson=dt.Find(id);
                if (jsonValid != null)
                {// nếu đã có thì update lại bản mới nhất
                    lstCV.Remove(jsonValid);
                    
                    lstCV.Add(newjson);
                }
                else
                {//không có thì thêm ms
                    lstCV.Add(newjson);
                }
            }
            else
            {
                lstCV.Add(congVien);
            }
            var lstjson = JsonConvert.SerializeObject(lstCV);
            HttpContext.Session.SetString("lstjson", lstjson);
            if (id != congVien.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dt.Update(congVien);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CongVienExists(congVien.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(congVien);
        }

        public async Task<IActionResult> Rollback(Guid id)
        {
            try
            {
                var lstjson = HttpContext.Session.GetString("lstjson");
                var lstCV = JsonConvert.DeserializeObject<List<CongVien>>(lstjson);
                var congVien = lstCV.FirstOrDefault(p => p.Id == id);
                dt.Update(congVien);
                lstCV.Remove(congVien);
                var lstjson2 = JsonConvert.SerializeObject(lstCV);
                HttpContext.Session.SetString("lstjson", lstjson2);

            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return RedirectToAction(nameof(Index));

            return View();
        }
		public async Task<IActionResult> Rollback_de(Guid id)
		{
			try
			{
				var lstjson = HttpContext.Session.GetString("xoa");
				var congVien= JsonConvert.DeserializeObject<CongVien>(lstjson);
				dt.Create(congVien);
				HttpContext.Session.Remove("xoa");

			}
			catch (DbUpdateConcurrencyException)
			{

			}
			return RedirectToAction(nameof(Index));

			
		}
		// GET: CongViens/Delete/5
		public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || dt.GetAll() == null)
            {
                return NotFound();
            }

            var congVien = dt.Find(id);
            if (congVien == null)
            {
                return NotFound();
            }

            return View(congVien);
        }

        // POST: CongViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (dt.GetAll() == null)
            {
                return Problem("Entity set 'MyContext.congViens'  is null.");
            }
            var congVien = dt.Find(id);
            if (congVien != null)
            {
                var xoajson= JsonConvert.SerializeObject(congVien);
                HttpContext.Session.SetString("xoa", xoajson);
                dt.Delete(id);
            }


            return RedirectToAction(nameof(Index));
        }

        private bool CongVienExists(Guid id)
        {
            return dt.Find(id) != null ? true : false;
        }
    }
}
