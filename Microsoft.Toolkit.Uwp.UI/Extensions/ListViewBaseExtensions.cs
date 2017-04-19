﻿// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Microsoft.Toolkit.Uwp.UI
{
    /// <summary>
    /// Provides attached dependency properties for the <see cref="ListViewBase"/>
    /// </summary>
    public static class ListViewBaseExtensions
    {
        /// <summary>
        /// Attached <see cref="DependencyProperty"/> for binding an <see cref="ICommand"/> instance to a <see cref="ListViewBase"/>
        /// This ICommand is executed when ListViewBase Item receives interaction by means of ItemClick. This requires IsItemClickEnabled to set to true.
        /// The ICommand is passed the Item that received interaction as a parameter
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(ListViewBaseExtensions),
            new PropertyMetadata(null, OnCommandPropertyChanged));

        /// <summary>
        /// Gets the <see cref="ICommand"/> instance assocaited with the specified <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> from which to get the associated <see cref="ICommand"/> instance</param>
        /// <returns>The <see cref="ICommand"/> instance associated with the the <see cref="DependencyObject"/> or null</returns>
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        /// <summary>
        /// Sets the <see cref="ICommand"/> instance assocaited with the specified <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> to associated the <see cref="ICommand"/> instance to</param>
        /// <param name="value">The <see cref="ICommand"/> instance to bind to the <see cref="DependencyObject"/></param>
        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        private static void OnCommandPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ListViewBase listViewBase = sender as ListViewBase;

            if (listViewBase != null)
            {
                listViewBase.ItemClick -= OnItemClicked;

                ICommand command = args.NewValue as ICommand;

                if (command != null)
                {
                    listViewBase.ItemClick += OnItemClicked;
                }
            }
        }

        private static void OnItemClicked(object sender, ItemClickEventArgs args)
        {
            ListViewBase listViewBase = sender as ListViewBase;

            if (listViewBase == null)
            {
                return;
            }

            ICommand command = GetCommand(listViewBase);
            if (command != null && command.CanExecute(args.ClickedItem))
            {
                command.Execute(args.ClickedItem);
            }
        }

        /// <summary>
        /// Attached <see cref="DependencyProperty"/> for binding a <see cref="Brush"/> as an alternate row background color to a <see cref="ListViewBase"/>
        /// </summary>
        public static readonly DependencyProperty AlternateRowColorProperty = DependencyProperty.RegisterAttached(
            "AlternateRowColor",
            typeof(Brush),
            typeof(ListViewBaseExtensions),
            new PropertyMetadata(null, OnAlternateRowColorPropertyChanged));

        /// <summary>
        /// Gets the alternate <see cref="Brush"/> associated with the specified <see cref="ListViewBase"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to get the associated <see cref="Brush"/> from</param>
        /// <returns>The <see cref="Brush"/> associated with the <see cref="ListViewBase"/></returns>
        public static Brush GetAlternateRowColor(ListViewBase obj)
        {
            return (Brush)obj.GetValue(AlternateRowColorProperty);
        }

        /// <summary>
        /// Sets the alternate <see cref="Brush"/> associated with the specified <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to associate the <see cref="Brush"/> with</param>
        /// <param name="value">The <see cref="Brush"/> for binding to the <see cref="ListViewBase"/></param>
        public static void SetAlternateRowColor(ListViewBase obj, Brush value)
        {
            obj.SetValue(AlternateRowColorProperty, value);
        }

        private static void OnAlternateRowColorPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ListViewBase listViewBase = sender as ListViewBase;

            if (listViewBase == null)
            {
                return;
            }

            listViewBase.ContainerContentChanging -= ColorContainerContentChanging;

            if (AlternateRowColorProperty != null)
            {
                listViewBase.ContainerContentChanging += ColorContainerContentChanging;
            }
        }

        private static void ColorContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            var itemContainer = args.ItemContainer as ListViewItem;
            var itemIndex = sender.IndexFromContainer(itemContainer);

            if (itemIndex % 2 == 0)
            {
                itemContainer.Background = GetAlternateRowColor(sender);
            }
            else
            {
                itemContainer.Background = null;
            }
        }

        /// <summary>
        /// Attached <see cref="DependencyProperty"/> for binding a <see cref="DataTemplate"/> as an alternate row template to a <see cref="ListViewBase"/>
        /// </summary>
        public static readonly DependencyProperty AlternateRowItemTemplateProperty = DependencyProperty.RegisterAttached(
            "AlternateRowItemTemplate",
            typeof(DataTemplate),
            typeof(ListViewBaseExtensions),
            new PropertyMetadata(null, OnAlternateRowItemTemplatePropertyChanged));

        /// <summary>
        /// Gets the <see cref="DataTemplate"/> associated with the specified <see cref="ListViewBase"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to get the associated <see cref="DataTemplate"/> from</param>
        /// <returns>The <see cref="DataTemplate"/> associated with the <see cref="ListViewBase"/></returns>
        public static DataTemplate GetAlternateRowItemTemplate(ListViewBase obj)
        {
            return (DataTemplate)obj.GetValue(AlternateRowItemTemplateProperty);
        }

        /// <summary>
        /// Sets the <see cref="DataTemplate"/> associated with the specified <see cref="ListViewBase"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to associate the <see cref="DataTemplate"/> with</param>
        /// <param name="value">The <see cref="DataTemplate"/> for binding to the <see cref="ListViewBase"/></param>
        public static void SetAlternateRowItemTemplate(ListViewBase obj, DataTemplate value)
        {
            obj.SetValue(AlternateRowItemTemplateProperty, value);
        }

        private static void OnAlternateRowItemTemplatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ListViewBase listViewBase = sender as ListViewBase;

            if (listViewBase == null)
            {
                return;
            }

            listViewBase.ContainerContentChanging -= ItemTemplateContainerContentChanging;

            if (AlternateRowItemTemplateProperty != null)
            {
                listViewBase.ContainerContentChanging += ItemTemplateContainerContentChanging;
            }
        }

        private static void ItemTemplateContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            var itemContainer = args.ItemContainer as ListViewItem;
            var itemIndex = sender.IndexFromContainer(itemContainer);

            if (itemIndex % 2 == 0)
            {
                itemContainer.ContentTemplate = GetAlternateRowItemTemplate(sender);
            }
            else
            {
                itemContainer.ContentTemplate = sender.ItemTemplate;
            }
        }
    }
}
