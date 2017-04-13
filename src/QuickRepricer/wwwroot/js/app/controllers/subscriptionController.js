var SubscriptionController = (function () {
    var getSubscriptionStatus = function (trialEndDate, trailDaysLeft,
        trialExpired, subscriptionEndDate) {
        var subscriptionStatus = null;

        if (trialEndDate !== undefined) {

            subscriptionStatus = "Your trial period ends ";

            var trialEndDateText = moment(trialEndDate, 'DD/MM/YYYY', true).format("DD-MM-YYYY");

            if (trialEndDateText === moment(new Date(), 'DD/MM/YYYY', true).format("DD-MM-YYYY")) {
                subscriptionStatus += "TODAY."
            } else {
                subscriptionStatus += "on " + trialEndDateText + ".";
            }

            if (trailDaysLeft > 0) {
                subscriptionStatus += " You have " + trailDaysLeft + " day(s) of free trail left.";
            } else {
                subscriptionStatus += " Please Upgrade.";
            }
        }
        else if (trialExpired !== undefined) {
            subscriptionStatus = "Your trial period already ended and you do not have an active subscription at present. Please Upgrade.";
        }
        else if (subscriptionEndDate !== undefined) {

            subscriptionStatus = "Your last active subscription expired on "
                + moment(subscriptionEndDate, 'DD/MM/YYYY', true).format("DD-MM-YYYY") + ". Please Upgrade.";
        }

        return subscriptionStatus;
    };

    return {
        getSubscriptionStatus: getSubscriptionStatus
    };

})();

