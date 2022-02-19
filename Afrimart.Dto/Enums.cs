using System;
using System.Collections.Generic;
using System.Text;

namespace Afrimart.Dto
{
    public enum FileType
    {
        DisplayImage,
        GalleryImages,
        ExplainerVideo
    }
    public enum AddressType
    {
        Shipping,
        Billing
    }

    public enum ShippingMethod
    {
        StandardDelivery,
        StorePickup
    }
    public enum OrderStatus
    {
        InProgress, // order just created 
        Shipped,
        Delayed,
        Delivered, // Delivered by postman or Picked up by Owner at store
        Canceled
    }
}
