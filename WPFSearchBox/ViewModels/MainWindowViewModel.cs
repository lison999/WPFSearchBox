using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Controls;

namespace WpfApp1.ViewModels
{
    public class MainWindowViewModel:BindableBase
    {
        public MainWindowViewModel()
        {
            SelectConditions = new ObservableCollection<object>();
            SearchConditionss = new ObservableCollection<SearchCondition>();
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

            search.ConditionTypePanel = conditionTypePanel;
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
            searchside.ConditionTypePanel = conditionTypePanelSide;

            SearchConditionss.Add(search);
            SearchConditionss.Add(searchside);
        }

        private ObservableCollection<SearchCondition> _searchConditions;
        /// <summary>
        /// 控件的itemsource
        /// </summary>
        public ObservableCollection<SearchCondition> SearchConditionss
        {
            get { return _searchConditions; }
            set { _searchConditions = value; RaisePropertyChanged(nameof(SearchConditionss)); }
        }


        private ObservableCollection<object> _selectConditions;
        /// <summary>
        /// 选中的条件，值由控件传回来
        /// </summary>
        public ObservableCollection<object> SelectConditions
        {
            get { return _selectConditions; }
            set { _selectConditions = value; RaisePropertyChanged(nameof(SelectConditions)); }
        }
    }
}
