// Updated by XamlIntelliSenseFileGenerator 16.02.2022 13:23:03
#pragma checksum "..\..\Registration.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FCD08CC7680015F5B4A658890805B9FC7597199F831BBDC79595E459F211C34C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
using WorkControl;


namespace WorkControl
{


    /// <summary>
    /// Registration
    /// </summary>
    public partial class Registration : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 43 "..\..\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridMenu;

#line default
#line hidden


#line 57 "..\..\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonOpenMenu;

#line default
#line hidden


#line 60 "..\..\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonCloseMenu;

#line default
#line hidden


#line 66 "..\..\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewMenu;

#line default
#line hidden


#line 67 "..\..\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListViewItem ItemHome;

#line default
#line hidden


#line 83 "..\..\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListViewItem Registration1;

#line default
#line hidden


#line 161 "..\..\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridBarraTitulo;

#line default
#line hidden


#line 169 "..\..\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonFechar;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WorkControl;component/registration.xaml", System.UriKind.Relative);

#line 1 "..\..\Registration.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.GridMenu = ((System.Windows.Controls.Grid)(target));
                    return;
                case 2:
                    this.ButtonOpenMenu = ((System.Windows.Controls.Button)(target));

#line 57 "..\..\Registration.xaml"
                    this.ButtonOpenMenu.Click += new System.Windows.RoutedEventHandler(this.ButtonOpenMenu_Click);

#line default
#line hidden
                    return;
                case 3:
                    this.ButtonCloseMenu = ((System.Windows.Controls.Button)(target));

#line 60 "..\..\Registration.xaml"
                    this.ButtonCloseMenu.Click += new System.Windows.RoutedEventHandler(this.ButtonCloseMenu_Click);

#line default
#line hidden
                    return;
                case 4:
                    this.ListViewMenu = ((System.Windows.Controls.ListView)(target));

#line 66 "..\..\Registration.xaml"
                    this.ListViewMenu.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListViewMenu_SelectionChanged);

#line default
#line hidden
                    return;
                case 5:
                    this.ItemHome = ((System.Windows.Controls.ListViewItem)(target));
                    return;
                case 6:

#line 71 "..\..\Registration.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Admin_Click);

#line default
#line hidden
                    return;
                case 7:
                    this.Registration1 = ((System.Windows.Controls.ListViewItem)(target));
                    return;
                case 8:

#line 93 "..\..\Registration.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Сhangingdata_Click);

#line default
#line hidden
                    return;
                case 9:

#line 109 "..\..\Registration.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Statistion_Click);

#line default
#line hidden
                    return;
                case 10:
                    this.secondNameTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 11:
                    this.rolesComboBox = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 12:
                    this.lastNameTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 13:
                    this.nameTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 14:
                    this.workPostTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 15:
                    this.workWeekComboBox = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 16:
                    this.loginNameTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 17:
                    this.generatePasswordButton = ((System.Windows.Controls.Button)(target));

#line 155 "..\..\Registration.xaml"
                    this.generatePasswordButton.Click += new System.Windows.RoutedEventHandler(this.generatePasswordButton_Click);

#line default
#line hidden
                    return;
                case 18:
                    this.passwordNameTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 19:
                    this.registerButton = ((System.Windows.Controls.Button)(target));

#line 159 "..\..\Registration.xaml"
                    this.registerButton.Click += new System.Windows.RoutedEventHandler(this.registerButton_Click);

#line default
#line hidden
                    return;
                case 20:
                    this.GridBarraTitulo = ((System.Windows.Controls.Grid)(target));

#line 161 "..\..\Registration.xaml"
                    this.GridBarraTitulo.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.GridBarraTitulo_MouseDown);

#line default
#line hidden
                    return;
                case 21:

#line 163 "..\..\Registration.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.backward_Click);

#line default
#line hidden
                    return;
                case 22:

#line 166 "..\..\Registration.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);

#line default
#line hidden
                    return;
                case 23:
                    this.ButtonFechar = ((System.Windows.Controls.Button)(target));

#line 169 "..\..\Registration.xaml"
                    this.ButtonFechar.Click += new System.Windows.RoutedEventHandler(this.ButtonFechar_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }
    }
}
