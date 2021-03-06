﻿using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventario.DAL
{
    public class RepositorioDeMaterial:IRepositorio<Material>

    {
        private string DBName = "Inventario.db";
        private string TableName = "Materiales";

        public List<Material> Read
        {
            get
            {
                List<Material> datos = new List<Material>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Material>(TableName).FindAll().ToList();

                }
                return datos;
            }
        }

        public bool Create(Material entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Material>(TableName);
                    coleccion.Insert(entidad);

                }
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public bool Delete(string id)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Material>(TableName);
                    coleccion.Delete(id);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

       

        public bool Update(Material entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Material>(TableName);
                    coleccion.Update(entidadModificada);

                }
                return true;
            }
            catch (Exception)
            {
                return false;


            }
        }
    }
}
