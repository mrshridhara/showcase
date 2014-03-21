using Showcase.Wpf.Base.Communication;
using Showcase.Wpf.Base.Presentation.Events;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Showcase.Wpf.Base.Presentation.Defaults
{
    public class RegionStore : IRegionStore
    {
        private readonly IDataTemplateStore _dataTemplateStore;
        private readonly ConcurrentDictionary<string, ContentControl> _regionDictionary;

        public RegionStore(IEventStore eventStore, IDataTemplateStore dataTemplateStore)
        {
            if (eventStore == null)
            {
                throw new ArgumentNullException("eventStore");
            }

            if (dataTemplateStore == null)
            {
                throw new ArgumentNullException("dataTemplateStore");
            }

            _dataTemplateStore = dataTemplateStore;
            _regionDictionary = new ConcurrentDictionary<string, ContentControl>();

            Task getRegisterEventTask = eventStore.GetEventOfTypeAsync<RegionRegisteredEvent>()
                                                  .Result
                                                  .HookAsync(OnRegionRegistered);

            Task getDeregisterEventTask = eventStore.GetEventOfTypeAsync<RegionDeregisteredEvent>()
                                                    .Result
                                                    .HookAsync(OnRegionDeregistered);

            Task.WaitAll(getRegisterEventTask, getDeregisterEventTask);
        }

        public async Task ClearRegionAsnc(string regionName)
        {
            if (regionName == null)
            {
                throw new ArgumentNullException("regionName");
            }

            if (regionName.Trim().Length == 0)
            {
                throw new ArgumentException("Region name cannot be empty.", "regionName");
            }

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                ContentControl container;
                if (_regionDictionary.TryGetValue(regionName, out container))
                {
                    container.Content = null;
                }
            }, DispatcherPriority.DataBind);
        }

        public async Task ShowInRegionAsync<TDataContext>(string regionName, TDataContext dataContext)
        {
            if (regionName == null)
            {
                throw new ArgumentNullException("regionName");
            }

            if (dataContext == null)
            {
                throw new ArgumentNullException("dataContext");
            }

            if (regionName.Trim().Length == 0)
            {
                throw new ArgumentException("Region name cannot be empty.", "regionName");
            }

            await Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                ContentControl container;
                if (_regionDictionary.TryGetValue(regionName, out container))
                {
                    DataTemplate dataTemplate = await _dataTemplateStore.GetDataTemplateAsyncFor<TDataContext>();
                    if (dataTemplate == null)
                    {
                        throw new PresentationException(
                            string.Format("Data template not defined for type {0}", typeof(TDataContext)));
                    }

                    var viewInstance = dataTemplate.LoadContent() as FrameworkElement;

                    if (viewInstance == null)
                    {
                        throw new PresentationException(
                            string.Format("Invalid data template defined for type {0}", typeof(TDataContext)));
                    }

                    viewInstance.DataContext = dataContext;
                    container.Content = viewInstance;
                }
            }, DispatcherPriority.DataBind);
        }

        private async Task OnRegionRegistered(RegionData regionData)
        {
            if (regionData == null
                || string.IsNullOrEmpty(regionData.RegionName)
                || regionData.Container == null)
            {
                return;
            }

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                _regionDictionary.AddOrUpdate(regionData.RegionName,
                                              key => regionData.Container,
                                              (key, value) => regionData.Container);
            }, DispatcherPriority.DataBind);
        }

        private async Task OnRegionDeregistered(string regionName)
        {
            if (string.IsNullOrEmpty(regionName))
            {
                return;
            }

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                ContentControl container;
                if (_regionDictionary.TryRemove(regionName, out container))
                {
                    container.Content = null;
                }
            }, DispatcherPriority.DataBind);
        }
    }
}