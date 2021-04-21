using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    class DrillBuilder
    {
        /// <summary>
        /// Функциия, выполняющая этапы построения
        /// </summary>
        /// <param name="ksPart">интерфейс модели</param>
        /// <param name="parameters">параметры модели</param>
        public void BuildDrillModel(ksPart ksPart, DrillParameters parameters)
        {
            //построение основы сверла
            BuildDrillBase(ksPart, parameters);
            //построение лапки
            BuildTenon(ksPart, parameters);
            //построение рабочей части
            BuildWorkPart(ksPart, parameters);
        }

        /// <summary>
        /// Функция построения основы сверла
        /// </summary>
        /// <param name="ksPart">интерфейс модели</param>
        /// <param name="parameters">параметры моодели</param>
        private void BuildDrillBase(ksPart ksPart, DrillParameters parameters)
        {
            ksEntity sketchDrillBase = 
                (ksEntity)ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definitionSketch = 
                (ksSketchDefinition)sketchDrillBase.GetDefinition();
            ksEntity constructionPlane = 
                (ksEntity)ksPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);

            //базовая плоскость эскиза
            definitionSketch.SetPlane(constructionPlane);
            //создание эскиза
            sketchDrillBase.Create();
            // интерфейс редактора эскиза
            ksDocument2D sketchEdit = (ksDocument2D)definitionSketch.BeginEdit();

            //Создание эскиза модели сверла
            if (sketchEdit != null)
            {
                double x1 = 0;
                double y1 = 0;
                double x2 = 0;
                double y2 = 0;

                x2 = parameters.DrillDiameter / 2;
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                x2 = 0;
                y2 = parameters.DrillLenght;
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                y1 = parameters.DrillLenght;
                x2 = parameters.DrillDiameter / 2;
                y2 = parameters.DrillLenght - parameters.DrillDiameter / 2;
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                x1 = parameters.DrillDiameter / 2;
                y1 = parameters.DrillLenght - parameters.DrillDiameter / 2;
                x2 = parameters.DrillDiameter / 2;
                y2 = parameters.DrillLenght - parameters.WorkingPartLenght;
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                x1 = parameters.DrillDiameter / 2;
                y1 = parameters.DrillLenght - parameters.WorkingPartLenght;
                x2 = parameters.NeckWidth / 2;
                y2 = parameters.DrillLenght - parameters.WorkingPartLenght;
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                x1 = parameters.NeckWidth / 2;
                y1 = parameters.DrillLenght - parameters.WorkingPartLenght;
                x2 = parameters.NeckWidth / 2;
                y2 = parameters.DrillLenght - 
                     (parameters.NeckLenght + parameters.WorkingPartLenght);
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                x1 = parameters.NeckWidth / 2;
                y1 = parameters.DrillLenght - 
                     (parameters.NeckLenght + parameters.WorkingPartLenght);
                x2 = (parameters.DrillDiameter / 2) * 0.25 + 
                     parameters.DrillDiameter / 2;
                y2 = parameters.DrillLenght - (parameters.NeckLenght + 
                                               parameters.WorkingPartLenght);
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                x1 = parameters.DrillDiameter / 2;
                y1 = 0;
                x2 = (parameters.DrillDiameter / 2)*0.25 + 
                     parameters.DrillDiameter / 2;
                y2 = parameters.DrillLenght - (parameters.NeckLenght + 
                                               parameters.WorkingPartLenght);
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                // завершение редактирования эскиза
                definitionSketch.EndEdit();
            }

            //Построение оси вращения
            var sketchAxis = (ksDocument2D)definitionSketch.BeginEdit();
            sketchAxis.ksLineSeg(0, 0, 0, parameters.DrillLenght, 3);
            definitionSketch.EndEdit();
            
            //Операция вращения
            BossRotate(ksPart, sketchDrillBase);
        }

        /// <summary>
        /// Функция, выполняющая операцию вращения
        /// </summary>
        /// <param name="ksPart">интерфейс модели</param>
        /// <param name="sketch">эскиз</param>
        private void BossRotate(ksPart ksPart, ksEntity sketch)
        {
            //Операция вращения
            ksEntity entityBossRotate = 
                (ksEntity)ksPart.NewEntity((short)Obj3dType.o3d_bossRotated);

            if (entityBossRotate != null)
            {
                ksBossRotatedDefinition bossRotateDef = 
                    (ksBossRotatedDefinition)entityBossRotate.GetDefinition();

                if (bossRotateDef != null)
                {
                    // Параметры вращения
                    bossRotateDef.SetThinParam(false);
                    // Эскиз операции вращения
                    bossRotateDef.SetSketch(sketch);
                    // Создать операцию
                    entityBossRotate.Create();
                }
            }
        }

        /// <summary>
        /// Функция построения лапки
        /// </summary>
        /// <param name="ksPart">интерфейс модели</param>
        /// <param name="parameters">параметры моодели</param>
        private void BuildTenon(ksPart ksPart, DrillParameters parameters)
        {
            ksEntity sketch = 
                (ksEntity)ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definitionSketch = 
                (ksSketchDefinition)sketch.GetDefinition();
            ksEntity constructionPlane = 
                (ksEntity)ksPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);

            //базовая плоскость эскиза
            definitionSketch.SetPlane(constructionPlane);
            //создание эскиза
            sketch.Create();
            // интерфейс редактора эскиза
            ksDocument2D sketchEdit = (ksDocument2D)definitionSketch.BeginEdit();
            //Создание эскиза модели сверла
            if (sketchEdit != null)
            {
                double x1 = 0;
                double y1 = 0;
                double x2 = 0;
                double y2 = 0;

                //первый квадрат
                x1 = parameters.TenonWidth / 2;
                y1 = 0;
                x2 = parameters.TenonWidth / 2;
                y2 = parameters.TenonLenght;
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                x1 = parameters.TenonWidth / 2;
                y1 = 0;
                x2 = (parameters.DrillDiameter / 2 + 2.5);
                y2 = 0;
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                x1 = (parameters.DrillDiameter / 2 + 2.5);
                y1 = parameters.TenonLenght;
                x2 = (parameters.DrillDiameter / 2 + 2.5);
                y2 = 0;
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                x1 = (parameters.DrillDiameter / 2 + 2.5);
                y1 = parameters.TenonLenght;
                x2 = parameters.TenonWidth / 2;
                y2 = parameters.TenonLenght;
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                //второй квадрат

                x1 = - parameters.TenonWidth / 2;
                y1 = 0;
                x2 = - parameters.TenonWidth / 2;
                y2 = parameters.TenonLenght;
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                x1 = - parameters.TenonWidth / 2;
                y1 = 0;
                x2 = - (parameters.DrillDiameter / 2 + 2.5);
                y2 = 0;
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                x1 = - (parameters.DrillDiameter / 2 + 2.5);
                y1 = parameters.TenonLenght;
                x2 = - (parameters.DrillDiameter / 2 + 2.5);
                y2 = 0;
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                x1 = - (parameters.DrillDiameter / 2 + 2.5);
                y1 = parameters.TenonLenght;
                x2 = - parameters.TenonWidth / 2;
                y2 = parameters.TenonLenght;
                sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);

                // завершение редактирования эскиза
                definitionSketch.EndEdit();
                
                //Операция вырезать выдавливанием
                CutExtrusion(ksPart, sketch);
            }
        }

        /// <summary>
        /// Функция, выполняющая операцию вырезать выдавливанием
        /// </summary>
        /// <param name="ksPart">интерфейс модели</param>
        /// <param name="sketch">эскиз</param>
        private void CutExtrusion(ksPart ksPart, ksEntity sketch)
        {
            //Операция вырезания ввыдавливанием
            ksEntity entityCutEntity = 
                (ksEntity)ksPart.NewEntity((short)Obj3dType.o3d_cutExtrusion);

            if (entityCutEntity != null)
            {
                ksCutExtrusionDefinition cutEntityDef = 
                    (ksCutExtrusionDefinition)entityCutEntity.GetDefinition();

                if (cutEntityDef != null)
                {
                    // Параметры ввыдавливания
                    cutEntityDef.SetThinParam(false);
                    cutEntityDef.SetSideParam(true, 
                        (short)ksEndTypeEnum.etThroughAll, 0, 0);
                    cutEntityDef.SetSideParam(false, 
                        (short)ksEndTypeEnum.etThroughAll, 0, 0);
                    //второе направление
                    ksExtrusionParam extrudeParam = cutEntityDef.ExtrusionParam();
                    extrudeParam.direction = 2;
                    // Эскиз операции ввыдавливания
                    cutEntityDef.SetSketch(sketch);
                    // Создать операцию
                    entityCutEntity.Create();
                }
            }
        }

        /// <summary>
        /// Функция построения рабочей части
        /// </summary>
        /// <param name="ksPart">интерфейс модели</param>
        /// <param name="parameters">параметры моодели</param>
        private void BuildWorkPart(ksPart ksPart, DrillParameters parameters)
        {
            // плоскость XOY
            ksEntity constructionPlane = 
                (ksEntity)ksPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            // плоскость смещенная от XOY
            ksEntity additionalPlane = 
                (ksEntity)ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition offsetPlaneDefinition = 
                (ksPlaneOffsetDefinition)additionalPlane.GetDefinition();

            offsetPlaneDefinition.SetPlane(constructionPlane);
            offsetPlaneDefinition.direction = false;
            offsetPlaneDefinition.offset = parameters.DrillLenght;
            additionalPlane.hidden = true;
            additionalPlane.Create();

            //по часовой
            //создание эскиза канавки
            ksEntity sketchClockwise = 
                (ksEntity)ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            //построение канавки
            SketchSlot(sketchClockwise, additionalPlane, parameters, 1);
            // создаем цилиндрическую спираль
            ksEntity spiralClockwise = 
                (ksEntity)ksPart.NewEntity((short)Obj3dType.o3d_cylindricSpiral);
            //построение цилидрической спирали
            SpiralBuild(spiralClockwise, additionalPlane, parameters,0);
            //операция вырезать по траектории
            CutEvolution(ksPart, sketchClockwise, spiralClockwise);

            //против часовой
            //создание эскиза канавки
            ksEntity sketchCounterClockwise = 
                (ksEntity)ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            //построение канавки
            SketchSlot(sketchCounterClockwise, additionalPlane, parameters, -1);
            // создаем цилиндрическую спираль
            ksEntity spiralCounterClockwise = 
                (ksEntity)ksPart.NewEntity((short)Obj3dType.o3d_cylindricSpiral);
            //построение цилидрической спирали
            SpiralBuild(spiralCounterClockwise, additionalPlane, parameters, 180);
            //операция вырезать по траектории
            CutEvolution(ksPart, sketchCounterClockwise, spiralCounterClockwise);
        }

        /// <summary>
        /// Функция построения эскиза канавок
        /// </summary>
        /// <param name="sketch"></param>
        /// <param name="additionalPlane">смещенная плоскость</param>
        /// <param name="parameters"></param>
        /// <param name="coef">коэфициент, выбирающий четверть для построения</param>
        private void SketchSlot(ksEntity sketch, 
            ksEntity additionalPlane, DrillParameters parameters, int coef)
        {
            ksSketchDefinition definitionSketch = 
                (ksSketchDefinition)sketch.GetDefinition();

            //базовая плоскость эскиза
            definitionSketch.SetPlane(additionalPlane);
            //создание эскиза
            sketch.Create();

            // интерфейс редактора эскиза
            ksDocument2D sketchEdit = (ksDocument2D)definitionSketch.BeginEdit();

            //Создание эскиза модели сверла
            if (sketchEdit != null)
            {
                double x1 = 0.1 * (parameters.DrillDiameter / 2);
                double y1 = 0.1 * (parameters.DrillDiameter / 2);
                double x2 = parameters.DrillDiameter;
                double y2 = 0.1 * (parameters.DrillDiameter / 2);

                sketchEdit.ksLineSeg(coef*x1, coef*y1, coef*x2, coef*y2, 1);
                sketchEdit.ksArcBy3Points(coef*x1, coef*y1, coef*x1, coef*x1 / 2, 
                    coef*x2, coef*y2, 1);
                definitionSketch.EndEdit();
            }
        }

        /// <summary>
        /// Функция построения цилиндрицеской спирали
        /// </summary>
        /// <param name="spiral">объект спираль</param>
        /// <param name="additionalPlane">смещенная плоскость</param>
        /// <param name="parameters">параметры модели</param>
        /// <param name="agle">угол наклона спирали</param>
        private void SpiralBuild(ksEntity spiral, 
            ksEntity additionalPlane, DrillParameters parameters, double agle)
        {
            ksCylindricSpiralDefinition spiralDefinition = 
                (ksCylindricSpiralDefinition)spiral.GetDefinition();

            //параметры построения спирали
            spiralDefinition.diamType = 0;
            spiralDefinition.buildDir = true;
            spiralDefinition.diam = parameters.DrillDiameter;
            spiralDefinition.buildMode = 1;
            spiralDefinition.step = parameters.DrillDiameter * 3.5;
            spiralDefinition.height = parameters.WorkingPartLenght;
            spiralDefinition.turnDir = true;
            //наклон спирали
            spiralDefinition.firstAngle = agle;

            //построение спирали
            spiralDefinition.SetPlane(additionalPlane);
            spiral.hidden = true;
            spiral.Create();
        }

        /// <summary>
        /// Функция, выполняющая операцию вырезать по траектории
        /// </summary>
        /// <param name="ksPart">интерфейс модели</param>
        /// <param name="sketch">эскиз операции</param>
        /// <param name="spiral">траектория операции</param>
        private void CutEvolution(ksPart ksPart, ksEntity sketch, ksEntity spiral)
        {
            //Операция вырезать по траектории
            ksEntity entityCutEvolution = 
                (ksEntity)ksPart.NewEntity((short)Obj3dType.o3d_cutEvolution);

            if (entityCutEvolution != null)
            {
                ksCutEvolutionDefinition cutEvolutionDef = 
                    (ksCutEvolutionDefinition)entityCutEvolution.GetDefinition();

                if (cutEvolutionDef != null)
                {
                    // Эскиз операции вращения
                    cutEvolutionDef.SetThinParam(false);
                    cutEvolutionDef.SetSketch(sketch);

                    var array = cutEvolutionDef.PathPartArray();
                    array.Add(spiral);
                    // Создать операцию
                    entityCutEvolution.Create();
                }
            }
        }
    }
}
