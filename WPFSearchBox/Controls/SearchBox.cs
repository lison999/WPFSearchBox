using Prism.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp1.Controls
{
    [TemplatePart(Name = "PART_SelectSearchConditionBtn", Type = typeof(Button))]
    [TemplatePart(Name = "PART_AllDelCondition", Type = typeof(Button))]
    [TemplatePart(Name = "PART_SearchConditionsPopup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART__SelectItemsControl", Type = typeof(ItemsControl))]
    [TemplatePart(Name = "PART_SearchItem", Type = typeof(SearchBoxItem))]    
    public class SearchBox : ItemsControl
    {
        #region Ctor
        static SearchBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBox), new FrameworkPropertyMetadata(typeof(SearchBox)));
        }

        public SearchBox()
        {
            this.Loaded += (s, e) =>
            {
                PART_SelectSearchConditionBtn.Click += SelectSearchConditionBtn_Click;

                //----两种绑定方式------
                //条件中的删除按钮 [前台绑定]
                DelConditionCommand = new DelegateCommand<object>(DelConditionCommandHandle);
                //总删除按钮 【后台绑定】
                PART_AllDelCondition.Command = DelConditionCommand;                
            };
        }

        #endregion

        #region Methods private
        //展开下拉列表
        private void SelectSearchConditionBtn_Click(object sender, RoutedEventArgs e)
        {
            PART_SearchConditionsPopup.IsOpen = false;
            PART_SearchConditionsPopup.IsOpen = true;
            PART_SearchConditionsPopup.PopupAnimation = System.Windows.Controls.Primitives.PopupAnimation.Scroll;

            if (PART_SearchConditionsPopup.IsOpen == false)
            {
                PART_SearchConditionsPopup.IsOpen = true;
            }
        }

        //清除选择的项
        private void DelConditionCommandHandle(object obj)
        {
            //不传入条件就是删除全部
            if (obj == null)
            {
                CopySelectedItemsSource.Clear();
                ClearButton();
            }
            else
            {
                CopySelectedItemsSource.Remove(obj);
                DelButton(obj);
            }
        }
        #endregion

        #region Methods public
        public void AddItem(SearchBoxItem item)
        {
            ItemContents.Add(item);
        }

        public void AddSelectedItem(object item)
        {
            CopySelectedItemsSource.Add(item);
            SelectedItemsSource = CopySelectedItemsSource;
        }
        public void AddButton(Button button)
        {
            button.IsEnabled = false;
            buttons.Add(button);
        }

        private void DelButton(object obj)
        {
            var button = buttons.FirstOrDefault(f => f.DataContext == obj);
            if (button != null)
            {
                button.IsEnabled = true;
                buttons.Remove(button);
            }
        }

        private void ClearButton()
        {
            foreach (var item in buttons)
            {
                item.IsEnabled = true;
            }
            buttons.Clear();
        }

        public bool IsSelectedItems(object item)
        {
           var obj = CopySelectedItemsSource.FirstOrDefault(f => f == item);
            if (obj == null)
            {
                return true;
            } else
            {
                return false;
            }
        }
        #endregion

        #region Property Private
        //展开下拉列表按钮
        private Button PART_SelectSearchConditionBtn;
        //删除所有选择条件按钮
        private Button PART_AllDelCondition;
        //下拉列表容器
        private Popup PART_SearchConditionsPopup;
        private readonly List<SearchBoxItem> ItemContents = new List<SearchBoxItem>();
        private ICommand _delConditionCommand;
        private ObservableCollection<object> CopySelectedItemsSource = new ObservableCollection<object>();
        private List<Button> buttons = new List<Button>();
        #endregion

        #region Property Public
        public ICommand DelConditionCommand
        {
            get { return _delConditionCommand; }
            set { _delConditionCommand = value; }
        }
        #endregion

        #region Property DP

        /// <summary>
        /// SearchBox子控件模板
        /// </summary>
        public SearchBoxItem ItemContent
        {
            get { return (SearchBoxItem)GetValue(ItemContentProperty); }
            set { SetValue(ItemContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemContentProperty =
            DependencyProperty.Register("ItemContent", typeof(SearchBoxItem), typeof(SearchBox), new PropertyMetadata(null));

        /// <summary>
        /// 传给viewModel的选中条件
        /// </summary>
        public IEnumerable SelectedItemsSource
        {
            get { return (IEnumerable)GetValue(SelectedItemsSourceProperty); }
            set { SetValue(SelectedItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemSources.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsSourceProperty =
            DependencyProperty.Register("SelectedItemsSource", typeof(IEnumerable), typeof(SearchBox), new PropertyMetadata(null));

        //public string Text
        //{
        //    get { return (string)GetValue(TextProperty); }
        //    set { SetValue(TextProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TextProperty =
        //    DependencyProperty.Register("Text", typeof(string), typeof(SearchBox), new PropertyMetadata(string.Empty));


        #endregion

        #region Override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_SelectSearchConditionBtn = GetTemplateChild("PART_SelectSearchConditionBtn") as Button;
            PART_AllDelCondition = GetTemplateChild("PART_AllDelCondition") as Button;            
            PART_SearchConditionsPopup = GetTemplateChild("PART_SearchConditionsPopup") as Popup;          
        }
        #endregion
    }

    public class SearchBoxItem : ItemsControl
    {
        #region Ctor
        static SearchBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBoxItem), new FrameworkPropertyMetadata(typeof(SearchBoxItem)));
        }

        public SearchBoxItem()
        {
            this.Loaded += (s, e) =>
            {
                var parent = this.FindParent<SearchBox>();
                searchBox = parent;

                //将父控件中的条件转化到本控件中。
                var ItemsSourceBind = BindingOperations.GetBinding(searchBox.ItemContent, SearchBoxItem.ItemsSourceProperty);
                var TitleBin = BindingOperations.GetBinding(searchBox.ItemContent, SearchBoxItem.TitleProperty);

                BindingOperations.SetBinding(this, SearchBoxItem.ItemsSourceProperty, ItemsSourceBind);
                BindingOperations.SetBinding(this, SearchBoxItem.TitleProperty, TitleBin);
                
                searchBox.AddItem(this);      
            };

            SearchConditionButtonCommand = new DelegateCommand<Button>(SearchConditionButtonCommandHandle);
        }

        private void SearchConditionButtonCommandHandle(Button obj)
        {
            searchBox.AddButton(obj);
            searchBox.AddSelectedItem(obj.DataContext);
        }

        #endregion

        #region Property Private
        //父控件
        private SearchBox searchBox { get; set; }

        //子空间中的按钮Command
        private ICommand _searchConditionButtonCommand;
        #endregion

        #region Property Public
        public ICommand SearchConditionButtonCommand
        {
            get { return _searchConditionButtonCommand; }
            set { _searchConditionButtonCommand = value; }
        }
        #endregion

        #region Public DP

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SearchBoxItem), new PropertyMetadata(string.Empty));

        public Button SearchConditionButton
        {
            get { return (Button)GetValue(SearchConditionButtonProperty); }
            set { SetValue(SearchConditionButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchConditionButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchConditionButtonProperty =
            DependencyProperty.Register("SearchConditionButton", typeof(Button), typeof(SearchBoxItem), new PropertyMetadata(null));

        #endregion

        #region Override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
        #endregion
    }

    #region DataSource
    public class SearchCondition
    {
        public string Title { get; set; }
        public ConditionTypePanel ConditionTypePanel { get; set; } = new ConditionTypePanel();

        public SearchCondition(SearchCondition condition)
        {
            Title = condition.Title;
            ConditionTypePanel = condition.ConditionTypePanel;
        }
        public SearchCondition()
        {

        }
    }

    public class ConditionTypePanel
    {
        public List<ConditionType> ConditionTypes { get; set; } = new List<ConditionType>();
    }

    public class ConditionType
    {
        public string Id { get; set; }

        public Type Type { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public Visibility Visibility { get; set; }

        public ConditionType(ConditionType model)
        {
            Type = model.Type;
            Value = model.Value;
            IsActive = model.IsActive;
            Visibility = model.Visibility;
        }
        public ConditionType()
        {

        }
    }
    #endregion

    public static class Ext
    {
        internal static T FindParent<T>(this DependencyObject dependencyObject) where T : class
        {
            while (dependencyObject != null)
            {
                var target = dependencyObject as T;
                if (target != null)
                    return target;

                dependencyObject = LogicalTreeHelper.GetParent(dependencyObject) ?? VisualTreeHelper.GetParent(dependencyObject);
            }
            return null;
        }

        public static IEnumerable Append<T>(
        this IEnumerable<T> source, params T[] tail)
        {
            return source.Concat(tail);
        }

        public static T GetValue<T>(this DependencyObject self, DependencyProperty property)
        {
            Contract.Requires(self != null);
            Contract.Requires(property != null);

            return self.GetValue(property).SafeCast<T>();
        }
        public static T SafeCast<T>(this object value)
        {
            return (value == null) ? default(T) : (T)value;
        }
    }
}
