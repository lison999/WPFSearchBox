using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Expression = System.Linq.Expressions.Expression;

namespace WpfApp1.Controls
{
    [TemplatePart(Name = "PART_SelectSearchConditionBtn", Type = typeof(Button))]
    [TemplatePart(Name = "PART_SearchConditionsPopup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_SearchConditionsItemsControlOne", Type = typeof(ItemsControl))]
    [TemplatePart(Name = "PART_TitleTextBlock", Type = typeof(TextBlock))]
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


                PART_TitleTextBlock.SetBinding(TextBlock.TextProperty, new Binding(TitlePath));
            };
        }
        #endregion

        #region Methoeds
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
        #endregion

        #region Private
        private Button PART_SelectSearchConditionBtn;
        private Popup PART_SearchConditionsPopup;
        private TextBlock PART_TitleTextBlock;
        private ItemsControl PART_SearchConditionsItemsControlOne;
        #endregion

        #region Public

        #endregion

        #region DP

        public SearchBoxItem SearchBoxItem
        {
            get { return (SearchBoxItem)GetValue(SearchBoxItemProperty); }
            set { SetValue(SearchBoxItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchBoxItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchBoxItemProperty =
            DependencyProperty.Register("SearchBoxItem", typeof(SearchBoxItem), typeof(SearchBox), new PropertyMetadata(null));


        public IEnumerable SearchConditions
        {
            get { return (IEnumerable)GetValue(SearchConditionsProperty); }
            set { SetValue(SearchConditionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchConditions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchConditionsProperty =
            DependencyProperty.Register("SearchConditions", typeof(IEnumerable), typeof(SearchBox), new FrameworkPropertyMetadata(null, SearchConditionHandle));

        private static void SearchConditionHandle(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            //((SearchBox)dependencyObject).xxx();
        }


        public DataTemplate xxx(DataTemplate tem)
        {
            StackPanel check = tem.LoadContent() as StackPanel;
            //datatempalte.FindName("PART_TitleTextBlock");
            var textb = FindControl.FindFirstVisualChild<TextBlock>(check, "PART_TitleTextBlock");

            textb.SetBinding(TextBlock.TextProperty, new Binding(TitlePath));
            //textb.Text = "{Binding " + Title + "}";
            //tem
            return tem;
        }

        public IEnumerable ConditionTypes
        {
            get { return (IEnumerable)GetValue(ConditionTypesProperty); }
            set { SetValue(ConditionTypesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchConditions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConditionTypesProperty =
            DependencyProperty.Register("ConditionTypes", typeof(IEnumerable), typeof(SearchBox), new FrameworkPropertyMetadata(null));


        public string TitlePath
        {
            get { return (string)GetValue(TitlePathProperty); }
            set { SetValue(TitlePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitlePathProperty =
            DependencyProperty.Register("TitlePath", typeof(string), typeof(SearchBox), new FrameworkPropertyMetadata(string.Empty,
                new PropertyChangedCallback(TitlePathCallBack)));

        private static void TitlePathCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string newValue = e.NewValue.ToString();
            ((SearchBox)d).TitlePathHandle(newValue);
        }

        public void TitlePathHandle(string newValue)
        {


                    
        }

        public string ConditionType
        {
            get { return (string)GetValue(ConditionTypeProperty); }
            set { SetValue(ConditionTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConditionTypeProperty =
            DependencyProperty.Register("ConditionType", typeof(string), typeof(SearchBox), new FrameworkPropertyMetadata(string.Empty));


        public ICommand DelConditionCommand
        {
            get { return (ICommand)GetValue(DelConditionCommandProperty); }
            set { SetValue(DelConditionCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelConditionCommandProperty =
            DependencyProperty.Register("DelConditionCommand", typeof(int), typeof(SearchBox));

        #endregion

        #region Override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_SelectSearchConditionBtn = GetTemplateChild("PART_SelectSearchConditionBtn") as Button;
            PART_SearchConditionsPopup = GetTemplateChild("PART_SearchConditionsPopup") as Popup;
            PART_SearchConditionsItemsControlOne = GetTemplateChild("PART_SearchConditionsItemsControlOne") as ItemsControl;
            PART_TitleTextBlock = GetTemplateChild("PART_TitleTextBlock") as TextBlock;
        }
        #endregion
    }

    public class SearchBoxNew : Selector
    {
        static SearchBoxNew()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBoxNew), new FrameworkPropertyMetadata(typeof(SearchBoxNew)));
        }

        public SearchBoxNew()
        {
            this.Loaded += (s, e) =>
            {
               
            };
        }

        #region DP


        public SearchBoxItem Item
        {
            get { return (SearchBoxItem)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(SearchBoxItem), typeof(SearchBoxNew), new PropertyMetadata(null));


        #endregion
    }

    [TemplatePart(Name = "PART_SelectSearchConditionBtn", Type = typeof(Button))]
    [TemplatePart(Name = "PART_SearchConditionsPopup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_SearchConditionsItemsControlOne", Type = typeof(ItemsControl))]
    [TemplatePart(Name = "PART_TitleTextBlock", Type = typeof(TextBlock))]
    public class SearchBoxItem : ContentControl
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
                //PART_SelectSearchConditionBtn.Click += SelectSearchConditionBtn_Click;

            };
        }
        #endregion

        #region Methoeds
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
        #endregion

        #region Private
        private Button PART_SelectSearchConditionBtn;
        private Popup PART_SearchConditionsPopup;
        private TextBlock PART_TitleTextBlock;
        private ItemsControl PART_SearchConditionsItemsControlOne;
        #endregion

        #region Public

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(SearchBoxItem), new PropertyMetadata(null));


        #endregion

        #region Override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_SelectSearchConditionBtn = GetTemplateChild("PART_SelectSearchConditionBtn") as Button;
            PART_SearchConditionsPopup = GetTemplateChild("PART_SearchConditionsPopup") as Popup;
            PART_SearchConditionsItemsControlOne = GetTemplateChild("PART_SearchConditionsItemsControlOne") as ItemsControl;
            PART_TitleTextBlock = GetTemplateChild("PART_TitleTextBlock") as TextBlock;


        }
        #endregion
    }

    public class SearchBoxConditionTypes : ItemsControl
    {
        #region Ctor
        static SearchBoxConditionTypes()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBoxConditionTypes), new FrameworkPropertyMetadata(typeof(SearchBoxConditionTypes)));
        }
        #endregion


        #region Public


        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(SearchBoxItem), new PropertyMetadata(null));


        #endregion

        #region Override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
        #endregion
    }

    public class Select : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var u = container as FrameworkElement;

            var tem = u.FindResource("SearchItemsDataTemplate") as DataTemplate;

            StackPanel check = tem.LoadContent() as StackPanel;
            //datatempalte.FindName("PART_TitleTextBlock");
            var textb = FindControl.FindFirstVisualChild<TextBlock>(check, "PART_TitleTextBlock");
            textb.Text = "abc";

            return tem;
        }
    }

    public class TitlePathConverter : DependencyObject, IValueConverter
    {
        public string TitleP
        {
            get { return (string)GetValue(TitlePProperty); }
            set { SetValue(TitlePProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleP.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitlePProperty =
            DependencyProperty.Register("TitleP", typeof(string), typeof(TitlePathConverter), new PropertyMetadata(string.Empty));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value.GetType();            
            var getti = TitleP;
            var ss= type.GetProperty("Title");
            //var p= type.GetFields().FirstOrDefault(x => x.Name == parameter.ToString());

            //string valu = ss.GetValue(value).ToString();

            return "Abc";           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class TitleMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                //binding
                object dataContext = values[0];
                //SearchBox TitlePath
                string titlePath = values[1] as string;
                string value = dataContext.GetType().GetProperties().FirstOrDefault(x => x.Name == titlePath).GetValue(dataContext)?.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConditionTypeMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                //binding
                var dataContext = values[0];
                //SearchBox ConditionType
                string conditionType = values[1] as string;
                string[] classLevel = conditionType.Split('.');

                Type type1 = dataContext.GetType();

                ConverterHandle.VisitProperties<SearchCondition>(dataContext);

                if (classLevel.Count() == 1)
                {
                    object value = dataContext.GetType().GetProperties().FirstOrDefault(x => x.Name == conditionType).GetValue(dataContext);
                    return value;
                }

                string lastProperty = classLevel[classLevel.Count() - 1];

                PropertyPath path = new PropertyPath(conditionType,null);

                Type type = null;
                foreach (var item in classLevel)
                {
                    //dataContext.GetType().GetProperty(item).PropertyType.GetProperty(item).GetValue(dataContext);
                    if (type == null)
                    {
                        type = dataContext.GetType().GetProperties().FirstOrDefault(x => x.Name == item).PropertyType;
                        continue;
                    }
                    else
                    {
                        var info = type.GetProperties().FirstOrDefault(x => x.Name == lastProperty);
                        if (info == null)
                        {
                            type = type.GetProperties().FirstOrDefault(x => x.Name == item).PropertyType;
                            continue;
                        }

                        var value = info.GetValue(lastProperty);
                        return value;
                    }           
                }

                return null;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterHandle
    {
        /// <summary>
        /// 对未知类型的对象的属性进行递归访问
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static void VisitProperties<T>(object obj)
        {
            var type = obj.GetType();
            var paraExpression = Expression.Parameter(typeof(T), "object");
            foreach (var prop in type.GetProperties())
            {
                var propType = prop.PropertyType;
                //判断是否为基本类型或String
                //访问方式的表达式树为：obj =>obj.Property
                if (propType.IsPrimitive || propType == typeof(String))
                {
                    VisitProperty<T>(obj, prop, paraExpression, paraExpression);
                }

                else
                {
                    //对于访问方式的表达式树为： obj=>obj.otherObj.Property。

                    Console.WriteLine("not primitive property: " + prop.Name);
                    var otherType = prop.PropertyType;
                    MemberExpression memberExpression = Expression.Property(paraExpression, prop);
                    //访问obj.otherObj里的所有公有属性
                    foreach (var otherProp in otherType.GetProperties())
                    {
                        VisitProperty<T>(obj, otherProp, memberExpression, paraExpression);
                    }
                }
                Console.WriteLine("--------------------------------");
            }
        }

        /// <summary>
        /// 执行表达式树为： obj=>obj.Property 或 obj=>obj.otherObj.Property的计算
        /// </summary>
        /// <param name="instanceExpression">最终访问属性的obj对象的表达式树的表示</param>
        /// <param name="parameterExpression">类型T的参数表达式树的表示</param>
        public static void VisitProperty<T>(Object obj, PropertyInfo prop, Expression instanceExpression, ParameterExpression parameterExpression)
        {
            Console.WriteLine("property name: " + prop.Name);

            MemberExpression memExpression = Expression.Property(instanceExpression, prop);
            //实现类型转换，如将Id的int类型转为object类型，便于下面的通用性
            Expression objectExpression = Expression.Convert(memExpression, typeof(object));
            Expression<Func<T, object>> lambdaExpression = Expression.Lambda<Func<T, object>>(objectExpression, parameterExpression);
            //打印表达式树
            Console.WriteLine("expression tree: " + lambdaExpression);
            Func<T, object> func = lambdaExpression.Compile();
            Console.WriteLine("value: " + func((T)obj)); //打印出得到的属性值
        }

        /// <summary>
        /// 根据属性名获取属性值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="name">属性名</param>
        /// <returns>属性的值</returns>
        public static object GetPropertyValue<T>(T t, string name)
        {
            Type type = t.GetType();
            PropertyInfo p = type.GetProperty(name);
            if (p == null)
            {
                throw new Exception(String.Format("该类型没有名为{0}的属性", name));
            }
            var param_obj = Expression.Parameter(typeof(T));
            var param_val = Expression.Parameter(typeof(object));

            //转成真实类型，防止Dynamic类型转换成object
            var body_obj = Expression.Convert(param_obj, type);

            var body = Expression.Property(body_obj, p);
            var getValue = Expression.Lambda<Func<T, object>>(body, param_obj).Compile();
            return getValue(t);
        }
    }

    public sealed class SearchBoxConditionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Visible;

            var type = (string)value;
            var condition = (string)parameter;
            return type.Equals(condition) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class FindControl:FrameworkElement
    {
        public static T FindFirstVisualChild<T>(DependencyObject obj, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T && child.GetValue(NameProperty).ToString() == childName)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindFirstVisualChild<T>(child, childName);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }
    }

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
}
