﻿using Microsoft.AspNetCore.Mvc;
using A2.Data;


namespace A2.Controllers
{

    [ApiController]
    [Route("webapi")]
    public class A2Controller : Controller {

        private readonly IA2Repo _repository;

        public A2Controller(IA2Repo repository)
        {
            _repository = repository;
        }
    }
}