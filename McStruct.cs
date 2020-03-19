using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseCoffee
{
    public class PointsRequest
    {
        public int? activatedOfferId { get; set; }
        public DateTime dateRequested { get; set; }
        public object extendedData { get; set; }
        public bool isPointsExpiry { get; set; }
        public int loyaltyCardId { get; set; }
        public string loyaltyCardInstance { get; set; }
        public int pointsAllocated { get; set; }
        public int pointsRequested { get; set; }
        public string transactionId { get; set; }
    }

    public class pointPage
    {
        public List<PointsRequest> pointsRequests { get; set; }
    }
    public class requestCoupon
    {
        public string offerId { get; set; }
    }

    public class offerActivationInfo
    {
        public object altImageDescription { get; set; }
        public object altImagePath { get; set; }
        public bool burnt { get; set; }
        public object burntAt { get; set; }
        public int codeExpiryInMinutes { get; set; }
        public int codeType { get; set; }
        public string description { get; set; }
        public DateTime endDate { get; set; }
        public object giftBatchId { get; set; }
        public object giftId { get; set; }
        public object giftedByConsumerId { get; set; }
        public object giftedOnDate { get; set; }
        public int id { get; set; }
        public bool isAGift { get; set; }
        public bool isReward { get; set; }
        public DateTime lastUpdatedAt { get; set; }
        public int merchantId { get; set; }
        public int offerId { get; set; }
        public string offerInstanceUniqueId { get; set; }
        public object paymentProviderSuccessUrl { get; set; }
        public object promoImageDescription { get; set; }
        public string promoImagePath { get; set; }
        public DateTime redeemedAt { get; set; }
        public string redemptionId { get; set; }
        public object redemptionImage { get; set; }
        public string redemptionText { get; set; }
        public DateTime redemptionTextExpiry { get; set; }
        public int snapshotId { get; set; }
        public DateTime startDate { get; set; }
        public string termsAndConditions { get; set; }
        public string title { get; set; }
        public object venueIds { get; set; }
    }

    public class Instance
    {
        public string instanceId { get; set; }
        public bool isActive { get; set; }
        public int loyaltyCardId { get; set; }
        public int pointsBalance { get; set; }
        public object redeemedOfferId { get; set; }
    }

    public class Offer
    {
        public int burntCount { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public object closestVenue { get; set; }
        public int codeExpiryInMinutes { get; set; }
        public int codeType { get; set; }
        public List<object> contentTagReferenceCodes { get; set; }
        public string contentUrl { get; set; }
        public object dailyEndTime { get; set; }
        public object dailyStartTime { get; set; }
        public List<object> daysOfWeek { get; set; }
        public string description { get; set; }
        public object distanceToClosestVenue { get; set; }
        public DateTime endDate { get; set; }
        public string extendedData { get; set; }
        public object giftBatchId { get; set; }
        public object giftId { get; set; }
        public object giftedBy { get; set; }
        public object giftedByConsumerId { get; set; }
        public object giftedCopy { get; set; }
        public object giftedOnDate { get; set; }
        public int id { get; set; }
        public string image { get; set; }
        public object imageAlt { get; set; }
        public string imageAltDescription { get; set; }
        public string imageDescription { get; set; }
        public bool isAGift { get; set; }
        public bool isActive { get; set; }
        public bool isAvailableAllStores { get; set; }
        public bool isGiftable { get; set; }
        public bool isMerchantFavourite { get; set; }
        public bool isPremiumPlacement { get; set; }
        public bool isRespawningOffer { get; set; }
        public bool isReward { get; set; }
        public bool isSticky { get; set; }
        public object lastBurntAt { get; set; }
        public object lastRedeemedAt { get; set; }
        public DateTime lastUpdatedAt { get; set; }
        public int merchantId { get; set; }
        public string offerInstanceUniqueId { get; set; }
        public object paymentAmount { get; set; }
        public object paymentTaxRate { get; set; }
        public int paymentType { get; set; }
        public int pointValue { get; set; }
        public object product { get; set; }
        public int redemptionCount { get; set; }
        public object respawnLimit { get; set; }
        public int respawnsInDays { get; set; }
        public int sortOrder { get; set; }
        public DateTime startDate { get; set; }
        public object termsAndConditions { get; set; }
        public string title { get; set; }
        public List<string> venueExternalIds { get; set; }
        public List<int> venueIds { get; set; }
        public int weighting { get; set; }
    }

    public class OfferPage
    {
        public string assetsPath { get; set; }
        public object cardImage { get; set; }
        public string cardImageDescription { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public List<object> contentTagReferenceCodes { get; set; }
        public object dailyEndTime { get; set; }
        public object dailyStartTime { get; set; }
        public string description { get; set; }
        public DateTime endDate { get; set; }
        public string expiryScheduleDetails { get; set; }
        public object extendedData { get; set; }
        public int initialPoints { get; set; }
        public List<Instance> instances { get; set; }
        public int instancesAvailable { get; set; }
        public string instructions { get; set; }
        public bool isActive { get; set; }
        public int loyaltyCardId { get; set; }
        public string loyaltyCardType { get; set; }
        public int maxInstances { get; set; }
        public object maxPointsPerDay { get; set; }
        public object maxPointsRequestsPerDay { get; set; }
        public List<Offer> offers { get; set; }
        public int pointsExpiryDays { get; set; }
        public int pointsRequired { get; set; }
        public string pointsText { get; set; }
        public List<object> reasonCodes { get; set; }
        public DateTime startDate { get; set; }
        public string subtitle { get; set; }
        public string termsAndConditions { get; set; }
        public string title { get; set; }
        public int weighting { get; set; }
    }

    public class ConsumerInfo
    {
        public int CurrentMerchant { get; set; }
        public string dateOfBirth { get; set; }
        public string emailAddress { get; set; }
        public object extendedData { get; set; }
        public string firstName { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        public object homeCity { get; set; }
        public string lastName { get; set; }
        public object mobileNumber { get; set; }
        public string postcode { get; set; }
        public bool pushMessageOptOut { get; set; }
        public string userName { get; set; }
    }

    class McDoCode
    {
        public string expiryDate { get; set; }
        public int verificationToken { get; set; }
    }

    class PageData
    {
        public int code { get; set; }
        public int currentPoints { get; set; }
    }
}
