﻿using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Domain;
using Domain.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils.Helpers;

namespace BusinessLayer
{
    public class ViajeBusiness : AbsExcelPackage, IViajeBusiness
    {
        // Business
        private readonly ILugarBusiness _lugarBusiness;
        // Repositories
        private readonly IViajeRepository _viajeRepository;

        // Cache
        private ICache<IDataEncuesta> _cache;

        public ViajeBusiness(IViajeRepository viajeRepository, ILugarBusiness lugarBusiness, ICache<IDataEncuesta> cache)
        {
            _lugarBusiness = lugarBusiness;
            _viajeRepository = viajeRepository;
            _cache = cache;
        }

        public Viaje Insert(Viaje viaje)
        {
            viaje.IdViaje = _viajeRepository.Insert(viaje);

            return viaje;
        }

        public int InsertByExcel(string pathFile)
        {
            int count = 0;

            using (ExcelPackage package = GetPackage(pathFile))
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[1];

                // Get column Index
                int identificacion_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Identificación")).Start.Column;
                int fecha_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Fecha")).Start.Column;

                int calle_o_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Calle_Origen")).Start.Column;
                int num_o_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Num_Origen")).Start.Column;
                int lat_o_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Latitud_origen")).Start.Column;
                int lng_o_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Longitud_origen")).Start.Column;
                int zona_o_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Zona_Origen")).Start.Column;
                int tLugar_o_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Tlugar_Origen")).Start.Column;

                int calle_d_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Calle_Destino")).Start.Column;
                int num_d_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Num_Destino")).Start.Column;
                int lat_d_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Latitud_destino")).Start.Column;
                int lng_d_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Longitud_destino")).Start.Column;
                int zona_d_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Zona_Destino")).Start.Column;
                int tLugar_d_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Tlugar_Destino")).Start.Column;

                int motivoViaje_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("MotivoViaje")).Start.Column;
                int horaInicio_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("HoraInicio")).Start.Column;
                int horaFin_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("HoraFin")).Start.Column;
                int transporte_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Transporte")).Start.Column;
                int observaciones_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Observaciones")).Start.Column;

                try
                {
                    // Recorro las filas del excel
                    for (int i = 2; i <= excelWorksheet.Dimension.Rows; i++)
                    {
                        // Get Lugar
                        Persona persona = new Persona()
                        {
                            Identificacion = excelWorksheet.Cells[i, identificacion_j].Value.GetString()
                        };

                        persona = (Persona)_cache.GetObject($"persona_{persona.Identificacion}");

                        if (persona == null)
                        {
                            throw new Exception($"Null Person {excelWorksheet.Cells[i, identificacion_j].Value.GetString()} - row: {i}");
                        }

                        // Id Lugar Origen
                        Lugar lugar_o = new Lugar()
                        {
                            Latitud = excelWorksheet.Cells[i, lat_o_j].Value.GetDouble(),
                            Longitud = excelWorksheet.Cells[i, lng_o_j].Value.GetDouble()
                        };

                        var lugar_o_cache = (Lugar)_cache.GetObject($"lugar_{lugar_o.Latitud};{lugar_o.Longitud}");

                        // Id Zona origen
                        Espacio zona_o = new Espacio()
                        {
                            Codigo = excelWorksheet.Cells[i, zona_o_j].Value.GetString()
                        };

                        zona_o = (Espacio)_cache.GetObject($"espacio_{zona_o.Codigo}");

                        // Not exists en db => insert
                        if (lugar_o_cache == null && lugar_o.Latitud != null && lugar_o.Longitud != null)
                        {
                            lugar_o.Calle = excelWorksheet.Cells[i, calle_o_j].Value.GetString();
                            lugar_o.Numero = excelWorksheet.Cells[i, num_o_j].Value.GetString();
                            lugar_o.IdZona = zona_o.IdEspacio;

                            lugar_o = _lugarBusiness.Insert(lugar_o);

                            _cache.SetObject($"lugar_{lugar_o.Latitud};{lugar_o.Longitud}", lugar_o);
                        }
                        else
                        {
                            lugar_o = lugar_o_cache;
                        }

                        // Id Lugar Destino
                        Lugar lugar_d = new Lugar()
                        {
                            Latitud = excelWorksheet.Cells[i, lat_d_j].Value.GetDouble(),
                            Longitud = excelWorksheet.Cells[i, lng_d_j].Value.GetDouble()
                        };

                        var lugar_d_cache = (Lugar)_cache.GetObject($"lugar_{lugar_d.Latitud};{lugar_d.Longitud}");

                        // Id Zona destino
                        Espacio zona_d = new Espacio()
                        {
                            Codigo = excelWorksheet.Cells[i, zona_d_j].Value.GetString()
                        };

                        zona_d = (Espacio)_cache.GetObject($"espacio_{zona_d.Codigo}");

                        // Not exists en db => insert
                        if (lugar_d_cache == null && lugar_d.Latitud != null && lugar_d.Longitud != null)
                        {
                            lugar_d.Calle = excelWorksheet.Cells[i, calle_d_j].Value.GetString();
                            lugar_d.Numero = excelWorksheet.Cells[i, num_d_j].Value.GetString();
                            lugar_d.IdZona = zona_d.IdEspacio;

                            lugar_d = _lugarBusiness.Insert(lugar_d);

                            _cache.SetObject($"lugar_{lugar_d.Latitud};{lugar_d.Longitud}", lugar_d);
                        }
                        else
                        {
                            lugar_d = lugar_d_cache;
                        }

                        // Id TipoLugarOrigen
                        Codigo tipoLugar_o = new Codigo()
                        {
                            Clave = excelWorksheet.Cells[i, tLugar_o_j].Value.GetString()
                        };

                        tipoLugar_o = (Codigo)_cache.GetObject($"codigo_TipoLugar;{tipoLugar_o.Clave}");

                        // Id TipoLugarDestino
                        Codigo tipoLugar_d = new Codigo()
                        {
                            Clave = excelWorksheet.Cells[i, tLugar_d_j].Value.GetString()
                        };

                        tipoLugar_d = (Codigo)_cache.GetObject($"codigo_TipoLugar;{tipoLugar_d.Clave}");

                        // Id motivo
                        Codigo motivo = new Codigo()
                        {
                            Clave = excelWorksheet.Cells[i, motivoViaje_j].Value.GetString()
                        };

                        motivo = (Codigo)_cache.GetObject($"codigo_MotivoViaje;{motivo.Clave}");

                        // Id transporte
                        Codigo transporte = new Codigo()
                        {
                            Clave = excelWorksheet.Cells[i, transporte_j].Value.GetString()
                        };

                        transporte = (Codigo)_cache.GetObject($"codigo_TipoTransporte;{transporte.Clave}");

                        // Viaje
                        Viaje viaje = new Viaje()
                        {
                            IdPersona = persona.IdPersona,
                            FechaStr = excelWorksheet.Cells[i, fecha_j].Value.GetString(),
                            Fecha = excelWorksheet.Cells[i, fecha_j].Value.GetDateTime(),
                            IdOrigen = lugar_o?.IdLugar,
                            IdTipoLugarOrigen = tipoLugar_o?.IdCodigo,
                            IdDestino = lugar_d?.IdLugar,
                            IdTipoLugarDestino = tipoLugar_d?.IdCodigo,
                            IdMotivo = motivo?.IdCodigo,
                            HoraInicio = excelWorksheet.Cells[i, horaInicio_j].Value.GetTimeSpan(),
                            HoraFin = excelWorksheet.Cells[i, horaFin_j].Value.GetTimeSpan(),
                            IdTransporte = transporte?.IdCodigo,
                            Observaciones = excelWorksheet.Cells[i, observaciones_j].Value.GetString()
                        };

                        viaje = Insert(viaje);
                        count = viaje.IdViaje == null ? count : ++count;

                        Console.WriteLine($"IdViaje {viaje.IdViaje} - Origen: {lugar_o?.Calle} {lugar_o?.Numero} ({lugar_o?.Latitud};{lugar_o?.Longitud} - destino: {lugar_d?.Calle} {lugar_d?.Numero} ({lugar_d?.Latitud};{lugar_d?.Longitud})");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return count;
        }
    }
}
