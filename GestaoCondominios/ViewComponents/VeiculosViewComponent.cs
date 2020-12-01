﻿using GestaoCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoCondominios.ViewComponents
{
    public class VeiculosViewComponent : ViewComponent
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public VeiculosViewComponent(IVeiculoRepositorio veiculoRepositorio)
        {
            _veiculoRepositorio = veiculoRepositorio;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            return View(await _veiculoRepositorio.ObterVeiculosPorUtilizador(id));
        }
    }
}
