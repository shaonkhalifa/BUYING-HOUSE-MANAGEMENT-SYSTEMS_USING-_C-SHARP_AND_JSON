﻿#pragma checksum "..\..\EditOrderData.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4247FA270974BD8AD0216154D042DCCE07A6238B56FBD1A0C30A1833A7AF7A20"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Buying_House_Management_StoreApp_Project;
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


namespace Buying_House_Management_StoreApp_Project {
    
    
    /// <summary>
    /// EditOrderData
    /// </summary>
    public partial class EditOrderData : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\EditOrderData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtorderId;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\EditOrderData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbproductname;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\EditOrderData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbtnyes;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\EditOrderData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker txtdatepicker;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\EditOrderData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtquantity;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\EditOrderData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUnitPrice;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\EditOrderData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpdate;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\EditOrderData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Back;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\EditOrderData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Close;
        
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
            System.Uri resourceLocater = new System.Uri("/Buying_House_Management_StoreApp_Project;component/editorderdata.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EditOrderData.xaml"
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
            this.txtorderId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.cmbproductname = ((System.Windows.Controls.ComboBox)(target));
            
            #line 17 "..\..\EditOrderData.xaml"
            this.cmbproductname.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbproductname_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.rbtnyes = ((System.Windows.Controls.RadioButton)(target));
            
            #line 19 "..\..\EditOrderData.xaml"
            this.rbtnyes.Click += new System.Windows.RoutedEventHandler(this.rbtnyes_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtdatepicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.txtquantity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtUnitPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.btnUpdate = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\EditOrderData.xaml"
            this.btnUpdate.Click += new System.Windows.RoutedEventHandler(this.btnUpdate_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Back = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\EditOrderData.xaml"
            this.Back.Click += new System.Windows.RoutedEventHandler(this.Back_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Close = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\EditOrderData.xaml"
            this.Close.Click += new System.Windows.RoutedEventHandler(this.Close_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

