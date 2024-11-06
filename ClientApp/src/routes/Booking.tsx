// pages/Booking.jsx
import { Form, useActionData, useNavigate, useLocation, redirect } from 'react-router-dom';
import { useState } from 'react';
import PaymentInfo from '../components/Booking/PaymentInfo';
import PersonalInfo from '../components/Booking/PersonalInfo';
import SeatSelection from '../components/Booking/SeatSelection';
// Simulated seat layout for demo purposes
const mockSeatLayout = [
  ['A1', 'A2', 'A3', 'A4', 'A5'],
  ['B1', 'B2', 'B3', 'B4', 'B5'],
  ['C1', 'C2', 'C3', 'C4', 'C5'],
];

export async function action({ request, params }) {
  const formData = await request.formData();
  const bookingData = {
    movieId: params.id,
    cinema: formData.get('cinema'),
    time: formData.get('time'),
    seatType: formData.get('seatType'),
    selectedSeats: formData.getAll('seats'), // Collect all selected seats
  };

  console.log('Booking Submitted:', bookingData); // Check data submission
  console.log(...formData)

  return redirect(`/movies/${params.id}`);
}

const Booking = () => {
  const { state } = useLocation(); // Access data from the Link's state
  const actionData = useActionData(); // Get action result
  const navigate = useNavigate();

  const [selectedSeats, setSelectedSeats] = useState([]); // Track selected seats
  const [step, setStep] = useState(1); // Step state
  const [personalData, setPersonalData] = useState({ name: '', email: '', phone: '' });
  const [paymentData, setPaymentData] = useState({ cardNumber: '', expiryDate: '', cvv: '' });

  const handleNextStep = () => {
    if (isStepValid()) setStep(step + 1);
  };
  const handlePrevStep = () => setStep((prev) => prev - 1);
  const closeModal = () => navigate(-1); // Close the modal

  const isPaymentComplete = paymentData.cardNumber && paymentData.expiryDate && paymentData.cvv;
  const isStepValid = () => {
    switch (step) {
      case 1:
        return selectedSeats.length > 0;
      case 2:
        return personalData.name !== '' && personalData.email !== '';
      case 3:
        return isPaymentComplete;
      default:
        return false;
    }
  };

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center text-black">
      <div className="bg-white p-6 rounded-lg w-full max-w-lg">
        <h2 className="text-xl font-bold mb-4">Book Your Seats</h2>

        <p><strong>Movie:</strong> {state.movieTitle}</p>
        <p><strong>Cinema:</strong> {state.cinema}</p>
        <p><strong>Time:</strong> {state.time}</p>

        {actionData?.success && <p className="text-green-500">Booking successful!</p>}

        <Form method="post" className="space-y-4">
          <input type="hidden" name="cinema" value={state.cinema} />
          <input type="hidden" name="time" value={state.time} />
          <input type="hidden" name="seatType" value={state.seatType} />

         {/* Keep components mounted but toggle visibility */}
         <div className={`${step === 1 ? 'block' : 'hidden'}`}>
            <SeatSelection selectedSeats={selectedSeats} setSelectedSeats={setSelectedSeats} />
          </div>
          <div className={`${step === 2 ? 'block' : 'hidden'}`}>
            <PersonalInfo personalData={personalData} setPersonalData={setPersonalData} />
          </div>
          <div className={`${step === 3 ? 'block' : 'hidden'}`}>
            <PaymentInfo paymentData={paymentData} setPaymentData={setPaymentData} />
          </div>

          <div className="flex justify-between">
            {step > 1 && (
              <button
                type="button"
                onClick={handlePrevStep}
                className="px-4 py-2 bg-gray-300 rounded"
              >
                Back
              </button>
            )}
            {step < 3 ? (
              <button
                type="button"
                onClick={handleNextStep}
                className="px-4 py-2 bg-blue-500 text-white rounded disabled:bg-slate-500"
                disabled={!isStepValid()}
                
              >
                Next
              </button>
            ) : (
              // Conditionally render the submit button based on payment completion
              <button
                type="submit"
                className={`px-4 py-2 ${isPaymentComplete ? 'bg-blue-500 text-white' : 'bg-gray-300 text-gray-500'} rounded`}
                disabled={!isPaymentComplete} // Disable if payment is incomplete
              >
                Confirm Booking
              </button>
            )}
          </div>

          <button
            type="button"
            onClick={closeModal}
            className="px-4 py-2 bg-gray-300 rounded mt-4"
          >
            Cancel
          </button>
        </Form>
      </div>
    </div>
  );
};

export default Booking;
