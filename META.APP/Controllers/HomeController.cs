

using META.DOMAIN;
using META.INFRA.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace META.APP.Controllers
{
    public class HomeController : Controller
    {
        private IEmissora _EmissoraRepository;
        private IAudiencia _AudienciaRepository;

        public HomeController(IEmissora emissora,IAudiencia audiencia)
        {
            _EmissoraRepository = emissora;
            _AudienciaRepository = audiencia;

        }
        public IActionResult Index()
        {
            var model = _EmissoraRepository.GetAudienciEmissora();
          
            

            return View(model);
        }
        [HttpPost]
        public IActionResult GetPorVisao([FromBody]Visao Modo)
        {
            try
            {
                var retorno = _EmissoraRepository.GetAudienciEmissoraPorVisao(Modo);


            return Json(new { success = true, responseText = retorno });
            }
            catch (Exception ex)
            {
                return View();
            }
          
        }
        [HttpGet]
        public IActionResult Emissoras()
        {
            try
            {
                var ListaEmissoras = _EmissoraRepository.GetEmissora();


                return Json(new { success = true, responseText = ListaEmissoras });

            }
            catch (Exception ex)
            {
                return View();
            }
          

            
            
        
        }

        [HttpPost]
        public IActionResult InsertEmissoras([FromBody]Emissoras emissoras)
        {
            try
            {
                
              var retorno = _EmissoraRepository.CadastrarEmissora(emissoras);
              

               return Json(new { success = true, responseText = retorno });
                           
            }catch(Exception ex)
            {
                return View();
            }
           
          
          
           
        }

        public IActionResult DeletarEmissora(int id)
        {

            _EmissoraRepository.DeletarEmissora(id);

           return RedirectToAction("Index","Home");
         
        }

        public IActionResult EditarEmissora(int id)
        {
            
            Emissoras model = _EmissoraRepository.GetOneEmissora(id);

            return View(model);

        }

        public IActionResult SalvarEdicao(Emissoras model)
        {
            if (ModelState.IsValid)
            { 
             var retorno = _EmissoraRepository.EditarEmissora(model);
             return RedirectToAction("Index", "Home");

            }
            return View();
           

        }

        [HttpPost]
        public IActionResult InsertAudiencia([FromBody]Audiencia audiencia)
        {
            try
            {

                var retorno = _AudienciaRepository.CadastrarAudiencia(audiencia);


                return Json(new { success = true, responseText = retorno });

            }
            catch (Exception ex)
            {
                return View();
            }




        }
    }
}
