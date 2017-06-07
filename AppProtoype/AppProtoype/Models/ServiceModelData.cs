using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProtoype.Models
{
    public class ServiceModelData
    {
        public ObservableCollection<ServiceModel> ServiceCollection
        {
            get
            {
                return new ObservableCollection<ServiceModel>
                {
                    new ServiceModel
                    {
                        ServiceID = 1,
                        RetailerID = 1,
                        ServiceName = "Tire Rotation",
                        ServiceDescription = @"Vestibulum cursus dolor non ipsum facilisis, sed tempor neque interdum. 
                        Integer scelerisque nisl tortor, a consectetur tellus feugiat id. Donec at neque eu sem gravida blandit. 
                        Cras porta purus sed nunc accumsan tristique. Etiam fermentum tempor nisi, non bibendum urna bibendum vel. 
                        Pellentesque dui nunc, egestas non fringilla ac, varius nec turpis. In non urna sed dui dictum consectetur non vel ipsum. 
                        Quisque venenatis finibus justo. Phasellus lorem ligula, consectetur ac egestas quis, lacinia a nibh. 
                        Phasellus sit amet suscipit orci, ac pretium leo. Praesent non sem vitae ligula finibus consequat eget vitae massa. 
                        Donec non rutrum nisi."
                    },

                    new ServiceModel
                    {
                        ServiceID = 2,
                        RetailerID = 1,
                        ServiceName = "Wheel Balancing",
                        ServiceDescription = @"Vestibulum cursus dolor non ipsum facilisis, sed tempor neque interdum. 
                        Integer scelerisque nisl tortor, a consectetur tellus feugiat id. Donec at neque eu sem gravida blandit. 
                        Cras porta purus sed nunc accumsan tristique. Etiam fermentum tempor nisi, non bibendum urna bibendum vel. 
                        Pellentesque dui nunc, egestas non fringilla ac, varius nec turpis. In non urna sed dui dictum consectetur non vel ipsum. 
                        Quisque venenatis finibus justo. Phasellus lorem ligula, consectetur ac egestas quis, lacinia a nibh. 
                        Phasellus sit amet suscipit orci, ac pretium leo. Praesent non sem vitae ligula finibus consequat eget vitae massa. 
                        Donec non rutrum nisi."
                    },

                    new ServiceModel
                    {
                        ServiceID = 3,
                        RetailerID = 2,
                        ServiceName = "Alignments",
                        ServiceDescription = @"Vestibulum cursus dolor non ipsum facilisis, sed tempor neque interdum. 
                        Integer scelerisque nisl tortor, a consectetur tellus feugiat id. Donec at neque eu sem gravida blandit. 
                        Cras porta purus sed nunc accumsan tristique. Etiam fermentum tempor nisi, non bibendum urna bibendum vel. 
                        Pellentesque dui nunc, egestas non fringilla ac, varius nec turpis. In non urna sed dui dictum consectetur non vel ipsum. 
                        Quisque venenatis finibus justo. Phasellus lorem ligula, consectetur ac egestas quis, lacinia a nibh. 
                        Phasellus sit amet suscipit orci, ac pretium leo. Praesent non sem vitae ligula finibus consequat eget vitae massa. 
                        Donec non rutrum nisi."
                    },

                    new ServiceModel
                    {
                        ServiceID = 4,
                        RetailerID = 2,
                        ServiceName = "Belt Service",
                        ServiceDescription = @"Vestibulum cursus dolor non ipsum facilisis, sed tempor neque interdum. 
                        Integer scelerisque nisl tortor, a consectetur tellus feugiat id. Donec at neque eu sem gravida blandit. 
                        Cras porta purus sed nunc accumsan tristique. Etiam fermentum tempor nisi, non bibendum urna bibendum vel. 
                        Pellentesque dui nunc, egestas non fringilla ac, varius nec turpis. In non urna sed dui dictum consectetur non vel ipsum. 
                        Quisque venenatis finibus justo. Phasellus lorem ligula, consectetur ac egestas quis, lacinia a nibh. 
                        Phasellus sit amet suscipit orci, ac pretium leo. Praesent non sem vitae ligula finibus consequat eget vitae massa. 
                        Donec non rutrum nisi."
                    },

                    new ServiceModel
                    {
                        ServiceID = 5,
                        RetailerID = 3,
                        ServiceName = "Others, I don't know",
                        ServiceDescription = @"Vestibulum cursus dolor non ipsum facilisis, sed tempor neque interdum. 
                        Integer scelerisque nisl tortor, a consectetur tellus feugiat id. Donec at neque eu sem gravida blandit. 
                        Cras porta purus sed nunc accumsan tristique. Etiam fermentum tempor nisi, non bibendum urna bibendum vel. 
                        Pellentesque dui nunc, egestas non fringilla ac, varius nec turpis. In non urna sed dui dictum consectetur non vel ipsum. 
                        Quisque venenatis finibus justo. Phasellus lorem ligula, consectetur ac egestas quis, lacinia a nibh. 
                        Phasellus sit amet suscipit orci, ac pretium leo. Praesent non sem vitae ligula finibus consequat eget vitae massa. 
                        Donec non rutrum nisi."
                    }
                };
            }
        }
    }
}
