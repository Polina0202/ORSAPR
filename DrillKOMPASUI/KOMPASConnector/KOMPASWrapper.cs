using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using DrillKOMPAS;

using KAPITypes;
using Kompas6Constants;
using Kompas6Constants3D;
using Kompas6API5;
using KompasAPI7;

namespace KOMPASConnector
{
    //TODO: XML комментарии?
    /// <summary>
    /// Класс для подключения к КОМПАС-3D
    /// </summary>
    public class KOMPASWrapper
    {
        /// <summary>
        /// Интерфейс API КОМПАС 3D.
        /// </summary>
        public KompasObject Kompas { get; set; }

        /// <summary>
        /// Документ 3D
        /// </summary>
        public ksDocument3D Document3D { get; set; }

        /// <summary>
        /// Сборка
        /// </summary>
        public ksPart KsPart { get; set; }

        /// <summary>
        /// Открытие компаса
        /// </summary>
        public void OpenKOMPAS()
        {
            try
            {
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                Kompas = (KompasObject) Activator.CreateInstance(t);
                
            }
            catch
            {
                Kompas = null;
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                Kompas = (KompasObject)Activator.CreateInstance(t);
            }

            Kompas.Visible = true;
            Kompas.ActivateControllerAPI();
        }

        /// <summary>
        /// Функция, построения модели
        /// </summary>
        /// <param name="parameters">параметры модели</param>
        public void BuildModel(DrillParameters parameters)
        {
            Document3D = (ksDocument3D)Kompas.Document3D();
            Document3D.Create(false, true);
            KsPart = (ksPart)Document3D.GetPart((short)Part_Type.pTop_Part);

            DrillBuilder drillBuilder = new DrillBuilder();
            drillBuilder.BuildDrillModel(KsPart, parameters);
        }

    }
}
