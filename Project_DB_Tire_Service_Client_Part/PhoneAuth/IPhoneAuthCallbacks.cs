using Android.Widget;
using Firebase;
using Firebase.Auth;

namespace Project_DB_Tire_Service_Client_Part.PhoneAuth
{
    public interface IPhoneAuthCallbacks
    {
        EditText mPhoneNumberField { get; }
        PhoneAuthProvider.ForceResendingToken mResendToken { get; }
        string mVerificationId { get; }
        bool mVerificationInProgress { get; }
        string TAG { get; }

        void OnCodeSent(string verificationId, PhoneAuthProvider.ForceResendingToken forceResendingToken);
        void OnVerificationCompleted(PhoneAuthCredential credential);
        void OnVerificationFailed(FirebaseException exception);
    }
}