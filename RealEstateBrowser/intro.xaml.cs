﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RealEstateBrowser
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class intro : Page
    {

        public intro()
        {
            this.InitializeComponent();
            questions.Navigate(typeof(Question1));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string loc = null;
            loc = App.searchParam.getSearchCity();

            try
            {
                if (!loc.Equals(null))
                {
                    locationSymbol.Text = "\xE81D";
                    locationDisplay.Text = loc;
                }
            } catch (Exception error)
            {

            }
            
        }

        private void previousStep_Click(object sender, RoutedEventArgs e)
        {
            errorMsg.Text = "";
            errorSymbol.Text = "";

            if (questions.CanGoBack)
            {
                questions.GoBack();
                
            }
            else if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void nextStep_Click(object sender, RoutedEventArgs e)
        {

            errorMsg.Text = "";
            errorSymbol.Text = "";

            if (questions.CanGoForward)
            {
                questions.GoForward();
            }
            else
            {
                Type currentFrame = questions.SourcePageType;

                if (currentFrame.Name == "Question1")
                {
                    questions.Navigate(typeof(Question2));
                    progressBar.Text = "\xECCA \xECCB \xECCA";
                }

                if (currentFrame.Name == "Question2")
                {
                    questions.Navigate(typeof(Question3));
                    progressBar.Text = "\xECCA \xECCA \xECCB";
                }

                if (currentFrame.Name == "Question3")
                {
                    if (App.searchParam.getBudgetFrom() > App.searchParam.getBudgetTo())
                    {
                        errorMsg.Text = "Your have entered an incorrect budget";
                        errorSymbol.Text = "\xE783";
                    }
                    else
                    {
                        if (!App.searchParam.getSearchResults().Any())
                        {
                            errorMsg.Text = "We couldn't find any matches for you criteria";
                            errorSymbol.Text = "\xE783";
                        } else
                        {
                            App.searchParam.complete = true;
                            this.Frame.Navigate(typeof(MapResults));
                        }
                        
                    }
                }

            }

        }
    }
}
