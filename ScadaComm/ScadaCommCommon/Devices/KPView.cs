﻿/*
 * Copyright 2015 Mikhail Shiryaev
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * 
 * Product  : Rapid SCADA
 * Module   : ScadaCommCommon
 * Summary  : The base class for device library user interface
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2006
 * Modified : 2015
 */

using Scada.Data;
using System;
using System.Collections.Generic;

namespace Scada.Comm.Devices
{
    /// <summary>
    /// The base class for device library user interface
    /// <para>Родительский класс пользовательского интерфейса библиотеки КП</para>
    /// </summary>
    public abstract class KPView
    {
        /// <summary>
        /// Прототип входного канала
        /// </summary>
        public class InCnlPrototype : InCnlProps
        {
            /// <summary>
            /// Конструктор
            /// </summary>
            public InCnlPrototype()
                : base()
            {
            }
            /// <summary>
            /// Конструктор
            /// </summary>
            public InCnlPrototype(string cnlName, int cnlTypeID)
                : base(0, cnlName, cnlTypeID)
            {
                CtrlCnlProps = null;
            }

            /// <summary>
            /// Получить или установить ссылку на прототип канала управления, связанного со входным каналом
            /// </summary>
            public CtrlCnlPrototype CtrlCnlProps { get; set; }
        }

        /// <summary>
        /// Прототип канала управления
        /// </summary>
        public class CtrlCnlPrototype : CtrlCnlProps
        {
            /// <summary>
            /// Конструктор
            /// </summary>
            public CtrlCnlPrototype()
                : base()
            {
            }
            /// <summary>
            /// Конструктор
            /// </summary>
            public CtrlCnlPrototype(string ctrlCnlName, int cmdTypeID)
                : base(0, ctrlCnlName, cmdTypeID)
            {
            }
        }

        /// <summary>
        /// Прототипы каналов КП
        /// </summary>
        public class KPCnlPrototypes
        {
            /// <summary>
            /// Конструктор
            /// </summary>
            public KPCnlPrototypes()
            {
                InCnls = new List<InCnlPrototype>();
                CtrCnls = new List<CtrlCnlPrototype>();
            }

            /// <summary>
            /// Получить прототипы входных каналов
            /// </summary>
            public List<InCnlPrototype> InCnls { get; protected set; }
            /// <summary>
            /// Получить прототипы каналов управления
            /// </summary>
            public List<CtrlCnlPrototype> CtrCnls { get; protected set; }
        }

        /// <summary>
        /// Свойства линии связи
        /// </summary>
        public class CommLineProperties
        {
            /// <summary>
            /// Конструктор, ограничивающий создание объекта без параметров
            /// </summary>
            protected CommLineProperties()
            {
            }
            /// <summary>
            /// Конструктор
            /// </summary>
            public CommLineProperties(int number, SortedList<string, Settings.CustomParam> customParams)
            {
                if (customParams == null)
                    throw new ArgumentNullException("customParams");

                Number = number;
                CustomParams = customParams;
                Modified = false;
            }

            /// <summary>
            /// Получить или установить номер линии связи
            /// </summary>
            public int Number { get; set; }
            /// <summary>
            /// Получить ссылку на пользовательские параметры линии связи
            /// </summary>
            public SortedList<string, Settings.CustomParam> CustomParams { get; protected set; }
            /// <summary>
            /// Получить или установить признак изменения пользовательских параметров
            /// </summary>
            public bool Modified { get; set; }
        }


        private string cmdLine;  // командная строка
        private AppDirs appDirs; // директории приложения


        /// <summary>
        /// Конструктор для настройки библиотеки КП
        /// </summary>
        public KPView() 
            : this(0)
        {
        }

        /// <summary>
        /// Конструктор для настройки конкретного КП
        /// </summary>
        public KPView(int number)
        {
            cmdLine = "";
            appDirs = new AppDirs();

            CanShowProps = false;
            Number = number;
            CommLineProps = null;
        }


        /// <summary>
        /// Получить описание библиотеки КП
        /// </summary>
        public abstract string KPDescr { get; }

        /// <summary>
        /// Получить прототипы каналов КП по умолчанию
        /// </summary>
        public virtual KPCnlPrototypes DefaultCnls
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Получить параметры опроса КП по умолчанию
        /// </summary>
        public virtual KPReqParams DefaultReqParams
        {
            get
            {
                return KPReqParams.Default;
            }
        }

        /// <summary>
        /// Получить возможность отображения свойств КП
        /// </summary>
        public bool CanShowProps { get; protected set; }


        /// <summary>
        /// Получить номер КП
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// Получить или установить командную строку
        /// </summary>
        public string CmdLine
        {
            get
            {
                return cmdLine;
            }
            set
            {
                cmdLine = value ?? "";
            }
        }

        /// <summary>
        /// Получить или установить свойства линии связи
        /// </summary>
        public CommLineProperties CommLineProps { get; set; }

        /// <summary>
        /// Получить или установить директории приложения
        /// </summary>
        public AppDirs AppDirs
        {
            get
            {
                return appDirs;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                appDirs = value;
            }
        }


        /// <summary>
        /// Отобразить свойства КП
        /// </summary>
        public virtual void ShowProps()
        {
        }
    }
}