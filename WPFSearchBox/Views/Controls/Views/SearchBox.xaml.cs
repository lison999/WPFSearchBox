using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.Views
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : Window
    {
        public UserControl1()
        {
            InitializeComponent();

            //_itemsControl.Items.Add(new object());
            //_itemsControl.Items.Add(new object());
            //_itemsControl.Items.Add(new object());
            //_itemsControl.Items.Add(new object());

            this.DataContext = new UserControl1ViewModel();
        }

        private void SelectSearchConditionBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectSearchConditions.IsOpen = false;
            SelectSearchConditions.IsOpen = true;
            SelectSearchConditions.PopupAnimation = System.Windows.Controls.Primitives.PopupAnimation.Scroll;

            if (SelectSearchConditions.IsOpen == false)
            {
                SelectSearchConditions.IsOpen = true;
            }
        }

        private void SelectCondition_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
        }
    }

    public class UserControl1ViewModel : BindableBase
    {
        public UserControl1ViewModel()
        {
            SelectConditionTypes = new ObservableCollection<ConditionType>();
            SearchConditions = new ObservableCollection<SearchCondition>();
            SearchCondition search = new SearchCondition();
            //第一层
            search.Title = "订单列表";

            ConditionTypePanel conditionTypePanel = new ConditionTypePanel();
            ConditionType type = new ConditionType();
            type.Id = Guid.NewGuid().ToString();
            type.Type = typeof(string);
            type.Value = "Limit";
            type.IsActive = true;
            type.Visibility = Visibility.Visible;
            conditionTypePanel.ConditionTypes.Add(type);

            var type2 = new ConditionType(type);
            type2.Value = "Market";
            type2.Id = Guid.NewGuid().ToString();
            conditionTypePanel.ConditionTypes.Add(type2);

            search.ConditionTypePanel=conditionTypePanel;
            //第二层
            SearchCondition searchside = new SearchCondition();
            searchside.Title = "订单方向";

            ConditionTypePanel conditionTypePanelSide = new ConditionTypePanel();
            ConditionType type3 = new ConditionType();
            type3.Id = Guid.NewGuid().ToString();
            type3.Type = typeof(string);
            type3.Value = "Buy";
            type3.IsActive = true;
            type3.Visibility = Visibility.Visible;
            conditionTypePanelSide.ConditionTypes.Add(type3);

            var type4 = new ConditionType(type3);
            type4.Value = "Sell";
            type4.Id = Guid.NewGuid().ToString();
            conditionTypePanelSide.ConditionTypes.Add(type4);
            searchside.ConditionTypePanel=conditionTypePanelSide;

            SearchConditions.Add(search);
            SearchConditions.Add(searchside);
        }

        private ICommand _selectConditionCommand;

        public ICommand SelectConditionCommand
        {
            get
            {
                if (_selectConditionCommand == null)
                    _selectConditionCommand = new DelegateCommand<object>((obj) =>
                    {
                        string id = obj as string;

                        ConditionType conditionType = SelectConditionTypes.FirstOrDefault(x => x.Id.Equals(id));

                        if (conditionType == null)
                        {
                           var model = GetConditionType(id);
                            SelectConditionTypes.Add(model);
                        }
                        else
                        {
                            var model = GetConditionType(id);
                            SelectConditionTypes.Remove(model);
                        }                       
                    });

                return _selectConditionCommand;
            }
        }

        private ConditionType GetConditionType(string id)
        {
            foreach (var item in SearchConditions)
            {
                var model = item.ConditionTypePanel.ConditionTypes.FirstOrDefault(x => x.Id.Equals(id));
                if (model != null)
                    return model;
            }
            return null;
        }

        private ICommand _delConditionCommand;

        public ICommand DelConditionCommand
        {
            get
            {
                if (_delConditionCommand == null)
                    _delConditionCommand = new DelegateCommand<object>((obj) =>
                    {
                        string id = obj as string;
                        if (string.IsNullOrEmpty(id))
                        {
                            SelectConditionTypes.Clear();
                            return;
                        }
                        ConditionType condition = SelectConditionTypes.FirstOrDefault(x => x.Id.Equals(id));
                        if (condition != null)
                            SelectConditionTypes.Remove(condition);
                    });

                return _delConditionCommand;
            }
        }

        private ObservableCollection<SearchCondition> _searchConditions;

        public ObservableCollection<SearchCondition> SearchConditions
        {
            get { return _searchConditions; }
            set { _searchConditions = value; RaisePropertyChanged(nameof(SearchConditions)); }
        }

        private ObservableCollection<ConditionType> _selectConditionTypes;

        public ObservableCollection<ConditionType> SelectConditionTypes
        {
            get { return _selectConditionTypes; }
            set { _selectConditionTypes = value; RaisePropertyChanged(nameof(SelectConditionTypes)); }
        }
    }

    public class SearchCondition
    {
        public string Title { get; set; }
        public ConditionTypePanel ConditionTypePanel { get; set; } =new ConditionTypePanel();

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
