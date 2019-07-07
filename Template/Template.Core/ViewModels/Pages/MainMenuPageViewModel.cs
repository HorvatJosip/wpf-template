using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace Template.Core
{
    public class MainMenuPageViewModel : BaseViewModel
    {
        private CultureInfo language;
        private readonly ILocalizerService localizerService;

        public ICommand SayHelloCommand { get; set; }
        public string Hello { get; set; }
        public List<CultureInfo> Languages { get; set; }
        public CultureInfo Language
        {
            get => language;
            set
            {
                if (!Equals(value, language))
                {
                    language = value;

                    localizerService.ChangeLanguage(value.Name);
                }
            }
        }

        public MainMenuPageViewModel() { }

        public MainMenuPageViewModel(ILocalizerService localizerService)
        {
            this.localizerService = localizerService;
            var languageList = Core.Configuration.Get("Cultures").Split(',');
            Languages = languageList.Select(culture => new CultureInfo(culture)).ToList();
            SayHelloCommand = new RelayCommand(() => Hello = Core.Localizer["Hello"]);
        }
    }
}
