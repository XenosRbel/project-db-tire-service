using Android.Widget;
using Firebase.Auth;
using Firebase;

namespace Project_DB_Tire_Service_Client_Part.PhoneAuth
{
    public class PhoneAuthCallbacks : PhoneAuthProvider.OnVerificationStateChangedCallbacks, IPhoneAuthCallbacks
    {
        public bool mVerificationInProgress { get; private set; }
        public string TAG { get; private set; }
        public EditText mPhoneNumberField { get; private set; }
        public string mVerificationId { get; private set; }
        public PhoneAuthProvider.ForceResendingToken mResendToken { get; private set; }

        public override void OnVerificationCompleted(PhoneAuthCredential credential)
        {
            // This callback will be invoked in two situations:
            // 1 - Instant verification. In some cases the phone number can be instantly
            //     verified without needing to send or enter a verification code.
            // 2 - Auto-retrieval. On some devices Google Play services can automatically
            //     detect the incoming verification SMS and perform verification without
            //     user action.     

            // [START_EXCLUDE silent]
            mVerificationInProgress = false;
            // [END_EXCLUDE]
        }

        public override void OnVerificationFailed(FirebaseException exception)
        {
            // This callback is invoked in an invalid request for verification is made,
            // for instance if the the phone number format is not valid.

            mVerificationInProgress = false;

            if (exception is FirebaseAuthInvalidCredentialsException) {
                // Invalid request
                // [START_EXCLUDE]
                TAG = ("Invalid phone number.");
                // [END_EXCLUDE]
            } else if (exception is FirebaseTooManyRequestsException) {
                // The SMS quota for the project has been exceeded

            }
        }

        public override void OnCodeSent(string verificationId, PhoneAuthProvider.ForceResendingToken forceResendingToken)
        {
            // The SMS verification code has been sent to the provided phone number, we
            // now need to ask the user to enter the code and then construct a credential
            // by combining the code with a verification ID.
            base.OnCodeSent(verificationId, forceResendingToken);

            mVerificationId = verificationId;
            mResendToken = forceResendingToken;
        }
    }
}

