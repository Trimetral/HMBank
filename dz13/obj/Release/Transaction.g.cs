﻿#pragma checksum "..\..\Transaction.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "945AE54A2C31B38D031B82412D5E0733313EFB2FCB0F1029868063D8282290EE"
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
    /// Transaction
    /// </summary>
    public partial class Transaction : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\Transaction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lSender;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Transaction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbSender;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\Transaction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lReciever;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\Transaction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbReciever;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\Transaction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbAmount;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\Transaction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider slAmount;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\Transaction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bAccept;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\Transaction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/dz13;component/transaction.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Transaction.xaml"
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
            this.lSender = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.tbSender = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.lReciever = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.cbReciever = ((System.Windows.Controls.ComboBox)(target));
            
            #line 42 "..\..\Transaction.xaml"
            this.cbReciever.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbReciever_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbAmount = ((System.Windows.Controls.TextBox)(target));
            
            #line 55 "..\..\Transaction.xaml"
            this.tbAmount.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbAmount_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.slAmount = ((System.Windows.Controls.Slider)(target));
            
            #line 61 "..\..\Transaction.xaml"
            this.slAmount.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.slAmount_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.bAccept = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\Transaction.xaml"
            this.bAccept.Click += new System.Windows.RoutedEventHandler(this.bAccept_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.bCancel = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\Transaction.xaml"
            this.bCancel.Click += new System.Windows.RoutedEventHandler(this.bCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

