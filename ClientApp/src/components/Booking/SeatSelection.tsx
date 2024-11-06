const mockSeatLayout = [
    ['A1', 'A2', 'A3', 'A4', 'A5'],
    ['B1', 'B2', 'B3', 'B4', 'B5'],
    ['C1', 'C2', 'C3', 'C4', 'C5'],
  ];
  
  const SeatSelection = ({ selectedSeats, setSelectedSeats }) => {
    const handleSeatClick = (seat) => {
      if (selectedSeats.includes(seat)) {
        setSelectedSeats(selectedSeats.filter((s) => s !== seat));
      } else {
        setSelectedSeats([...selectedSeats, seat]);
      }
    };
  
    return (
      <div className="space-y-2">
        <h3 className="text-lg font-semibold">Select Your Seats</h3>
        <div className="grid grid-cols-5 gap-2">
          {mockSeatLayout.map((row) =>
            row.map((seat) => (
              <label key={seat} className="flex flex-col items-center">
                {/* Render hidden input for each selected seat */}
                {selectedSeats.includes(seat) && (
                  <input type="hidden" name="selectedSeats" value={seat} />
                )}
                <button
                  type="button"
                  onClick={() => handleSeatClick(seat)}
                  className={`p-2 rounded ${
                    selectedSeats.includes(seat) ? 'bg-blue-500 text-white' : 'bg-gray-300'
                  }`}
                  aria-pressed={selectedSeats.includes(seat)}
                >
                  {seat}
                </button>
                {selectedSeats.includes(seat) && (
                  <span className="text-sm text-blue-500">Selected</span>
                )}
              </label>
            ))
          )}
        </div>
      </div>
    );
  };
  
  export default SeatSelection;
  