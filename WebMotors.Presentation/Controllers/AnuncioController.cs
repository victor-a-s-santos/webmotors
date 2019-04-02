using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using WebMotors.Common;
using WebMotors.Presentation.Models;
using WebMotors.Repository.Interfaces;
using WebMotors.TransferObject.Entities;

namespace WebMotors.Presentation.Controllers
{
    public class AnuncioController : Controller
    {
        private IConfiguration _configuration;
        private IRepositoryAnuncio _repositoryAnuncio;

        public AnuncioController(
            IConfiguration Configuration,
            IRepositoryAnuncio repositoryAnuncio
            )
        {
            _configuration = Configuration;
            _repositoryAnuncio = repositoryAnuncio;
        }

        ApiClient<Marca, string> clientMarcas = new ApiClient<Marca, string>();
        ApiClient<Modelo, string> clientModelo = new ApiClient<Modelo, string>();
        ApiClient<Versao, string> clientVersao = new ApiClient<Versao, string>();

        // GET: Anuncio
        public ActionResult Index()        {

            return View(_repositoryAnuncio.GetAll());
        }

        // GET: Anuncio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Anuncio/Create
        public ActionResult Create()
        {
            var lstMarcas = clientMarcas.GetManyAsync("OnlineChallenge/Make").Result.ToList();

            lstMarcas.Insert(0, new Marca { ID = 0, Name = "Selecione..." });

            ViewBag.Marcas = new SelectList(lstMarcas,"ID","Name");

            return View();
        }

        // POST: Anuncio/Create
        [HttpPost]
        public JsonResult Create(Anuncio anuncio)
        {
            _repositoryAnuncio.Insert(anuncio);

            return Json(Url.Action("Index", "Anuncio"));
        }

        // GET: Anuncio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Anuncio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Anuncio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Anuncio/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public JsonResult PopulateModelo(int ID)
        {
            var lstModelos = clientModelo.GetManyAsync("OnlineChallenge/Model?MakeID=" + ID).Result.ToList();

            lstModelos.Insert(0, new Modelo { ID = 0, Name = "Selecione..." });

            return Json(new SelectList(lstModelos, "ID", "Name"));
        }

        public JsonResult PopulateVersao(int ID)
        {
            var lstVersoes = clientVersao.GetManyAsync("OnlineChallenge/Version?ModelID=" + ID).Result.ToList();

            lstVersoes.Insert(0, new Versao { ID = 0, Name = "Selecione..." });

            return Json(new SelectList(lstVersoes, "ID", "Name"));
        }
    }
}