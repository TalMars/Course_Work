﻿

#pragma checksum "C:\Users\TalMars\documents\visual studio 2013\Projects\UniverSmotri_WP8.1\UniverSmotri_WP8.1\NewsElement_V2.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AB1A40E00EE0D3188F671605C82C11AC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UniverSmotri_WP8._1
{
    partial class NewsElement_V2 : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 33 "..\..\NewsElement_V2.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).DoubleTapped += this.playerYouTube_DoubleTapped;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 24 "..\..\NewsElement_V2.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Back_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


