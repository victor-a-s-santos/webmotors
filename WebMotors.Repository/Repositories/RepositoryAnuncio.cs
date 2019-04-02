using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebMotors.Repository.Interfaces;
using WebMotors.TransferObject.Entities;

namespace WebMotors.Repository
{
    public class RepositoryAnuncio : RepositoryBase<Anuncio>, IRepositoryAnuncio
    {
        public RepositoryAnuncio(Context context) : base(context) { }
    }
}
