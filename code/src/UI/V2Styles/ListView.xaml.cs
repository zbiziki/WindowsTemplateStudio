﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Templates.UI.V2Controls;
using Microsoft.Templates.UI.V2Services;
using Microsoft.Templates.UI.V2ViewModels.Common;

namespace Microsoft.Templates.UI.V2Styles
{
    public partial class ListView
    {
        private async void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null)
            {
                switch (item.Content)
                {
                    case BasicInfoViewModel info:
                        SelectItem(info);
                        break;
                    case Step step:
                        await SelectStepAsync(step);
                        break;
                    default:
                        break;
                }
            }
        }

        private async void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var listView = sender as System.Windows.Controls.ListView;
            if (listView != null && e.Key == Key.Enter)
            {
                switch (listView.SelectedItem)
                {
                    case BasicInfoViewModel info:
                        SelectItem(info);
                        break;
                    case Step step:
                        await SelectStepAsync(step);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SelectItem(BasicInfoViewModel item)
        {
            switch (item)
            {
                case MetadataInfoViewModel metadataInfo:
                    if (!BaseMainViewModel.BaseInstance.IsSelectionEnabled(metadataInfo.MetadataType))
                    {
                        return;
                    }

                    break;
                default:
                    break;
            }

            BaseMainViewModel.BaseInstance.ProcessItem(item);
        }

        private async Task SelectStepAsync(Step step)
        {
            await BaseMainViewModel.BaseInstance.SetStepAsync(step.Index);
        }

        private void OnProjectDetailsTemplatesPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var listView = sender as System.Windows.Controls.ListView;
            if (listView != null)
            {
                var savedTemplate = listView.SelectedItem as SavedTemplateViewModel;
                if (savedTemplate != null)
                {
                    if (e.Key == Key.F2)
                    {
                        savedTemplate.Focus();
                    }
                    else if (e.Key == Key.Delete)
                    {
                        int currentIndex = listView.SelectedIndex;
                        savedTemplate.OnDelete();
                        if (currentIndex > 0)
                        {
                            currentIndex--;
                        }

                        listView.SelectedIndex = currentIndex;
                        var item = listView.ItemContainerGenerator.ContainerFromIndex(currentIndex) as ListViewItem;
                        item?.Focus();
                    }
                }
            }
        }

    }
}
