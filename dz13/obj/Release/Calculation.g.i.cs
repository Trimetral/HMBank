﻿#pragma checksum "..\..\Calculation.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "61D62F1F0216B52C11FC7A7740CC4553F29DB8B069EAF491C9208B4E11D73B41"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using dz13;


namespace dz13 {
    
    
    /// <summary>
    /// Calculation
    /// </summary>
    public partial class Calculation : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\Calculation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbInv;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Calculation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bNew;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Calculation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bWid;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\Calculation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bInc;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\Calculation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbArg;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\Calculation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cbCapit;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\Calculation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbDuration;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\Calculation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbDr;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\Calculation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bCalculate;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\Calculation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listCalculation;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\Calculation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bQuit;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/dz13;component/calculation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Calculation.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\Calculation.xaml"
            ((dz13.Calculation)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tbInv = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.bNew = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\Calculation.xaml"
            this.bNew.Click += new System.Windows.RoutedEventHandler(this.bNew_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.bWid = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\Calculation.xaml"
            this.bWid.Click += new System.Windows.RoutedEventHandler(this.bWid_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.bInc = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\Calculation.xaml"
            this.bInc.Click += new System.Windows.RoutedEventHandler(this.bInc_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cbArg = ((System.Windows.Controls.ComboBox)(target));
            
            #line 57 "..\..\Calculation.xaml"
            this.cbArg.DropDownOpened += new System.EventHandler(this.cbArg_DropDownOpened);
            
            #line default
            #line hidden
            
            #line 57 "..\..\Calculation.xaml"
            this.cbArg.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbArg_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.cbCapit = ((System.Windows.Controls.CheckBox)(target));
            
            #line 65 "..\..\Calculation.xaml"
            this.cbCapit.Checked += new System.Windows.RoutedEventHandler(this.cbCapit_Checked);
            
            #line default
            #line hidden
            
            #line 65 "..\..\Calculation.xaml"
            this.cbCapit.Unchecked += new System.Windows.RoutedEventHandler(this.cbCapit_Unchecked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.tbDuration = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.tbDr = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.bCalculate = ((System.Windows.Controls.Button)(target));
            
            #line 104 "..\..\Calculation.xaml"
            this.bCalculate.Click += new System.Windows.RoutedEventHandler(this.bCalculate_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.listCalculation = ((System.Windows.Controls.ListBox)(target));
            return;
            case 12:
            this.bQuit = ((System.Windows.Controls.Button)(target));
            
            #line 122 "..\..\Calculation.xaml"
            this.bQuit.Click += new System.Windows.RoutedEventHandler(this.bQuit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

