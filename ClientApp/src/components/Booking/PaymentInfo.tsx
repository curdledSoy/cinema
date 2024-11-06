import { useRef, useEffect, useState } from 'react';

// Helper function to detect card type based on card number
const getCardType = (number) => {
  const patterns = {
    visa: /^4/,
    mastercard: /^5[1-5]/,
    amex: /^3[47]/,
  };

  if (patterns.visa.test(number)) return 'visa';
  if (patterns.mastercard.test(number)) return 'mastercard';
  if (patterns.amex.test(number)) return 'amex';

  return 'unknown';
};

const formatCardNumber = (number) => {
  return number
    .replace(/\D/g, '') // Remove non-digits
    .replace(/(.{4})/g, '$1 ') // Add a space after every 4 digits
    .trim(); // Remove trailing spaces
};

const PaymentInfo = ({ paymentData, setPaymentData }) => {
  const [cardType, setCardType] = useState('unknown');
  const ccv = useRef(null);
  const cardNumber = useRef(null);
  const expiry = useRef(null);

  const triggerValidation = (input) => {
    console.log(input.value)
    if (input && input.value != 0 && input.value != '') input.reportValidity();
  };

  useEffect(() => {
    const inputs = [ccv.current, cardNumber.current, expiry.current];

    inputs.forEach((input) => {
      input?.addEventListener('blur', () => triggerValidation(input));
    });

    return () => {
      inputs.forEach((input) => {
        input?.removeEventListener('keyup', () => {});
        input?.removeEventListener('blur', () => {});
      });
    };
  }, []);

  const handleCardNumberChange = (e) => {
    const formattedNumber = formatCardNumber(e.target.value);
    setPaymentData({ ...paymentData, cardNumber: formattedNumber });
    setCardType(getCardType(formattedNumber));
  };

  return (
    <div className="space-y-4">
      <h3 className="text-lg font-semibold">Payment Information</h3>

      <div className="relative">
        <input
          type="text"
          name="cardNumber"
          placeholder="Card Number"
          value={paymentData.cardNumber}
          onChange={handleCardNumberChange}
          required
          className="border rounded p-2 w-full pl-12 text-lg tracking-wide"
          ref={cardNumber}
          pattern="^\d{4} \d{4} \d{4} \d{4}|\d{4} \d{6} \d{5}$" // Visa/MasterCard: 16 digits; AmEx: 15 digits
          title="Enter a valid card number."
        />
        <div className="absolute top-1/2 left-3 transform -translate-y-1/2">
          {cardType !== 'unknown' && (
            <img
              src={`/icons/${cardType}.svg`}
              alt={`${cardType} logo`}
              className="w-10"
            />
          )}
        </div>
      </div>

      <input
        type="text"
        name="expiryDate"
        placeholder="Expiry Date (MM/YY)"
        value={paymentData.expiryDate}
        onChange={(e) =>
          setPaymentData({ ...paymentData, expiryDate: e.target.value })
        }
        required
        className="border rounded p-2 w-full text-lg"
        ref={expiry}
        pattern="^(0[1-9]|1[0-2])\/\d{2}$" // MM/YY format
        title="Enter a valid expiry date (MM/YY)."
      />

      <input
        type="text"
        name="cvv"
        placeholder="CVV"
        value={paymentData.cvv}
        onChange={(e) =>
          setPaymentData({ ...paymentData, cvv: e.target.value })
        }
        required
        className="border rounded p-2 w-full text-lg"
        ref={ccv}
        pattern="^\d{3,4}$" // 3-4 digits for CVV
        title="Enter a valid CVV."
      />
    </div>
  );
};

export default PaymentInfo;
