﻿using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp.strategy
{
    public partial class FormStrategyExecutorState : Form
    {
        public FormStrategyExecutorState(IStrategyExecutorPool executorPool)
        {
            InitializeComponent();
        }
    }
}