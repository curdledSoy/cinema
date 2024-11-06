
import { useState } from 'react';

const PersonalInfo = ({ personalData, setPersonalData }) => {
  return (
    <div className="space-y-2">
      <h3 className="text-lg font-semibold">Personal Information</h3>
      <input
        type="text"
        name="name"
        placeholder="Full Name"
        value={personalData.name}
        onChange={(e) => setPersonalData({ ...personalData, name: e.target.value })}
        required
        className="border rounded p-2 w-full"
      />
      <input
        type="email"
        name="email"
        placeholder="Email"
        value={personalData.email}
        onChange={(e) => setPersonalData({ ...personalData, email: e.target.value })}
        required
        className="border rounded p-2 w-full"
      />
      <input
        type="tel"
        name="phone"
        placeholder="Phone Number"
        value={personalData.phone}
        onChange={(e) => setPersonalData({ ...personalData, phone: e.target.value })}
        required
        className="border rounded p-2 w-full"
      />
    </div>
  );
};

export default PersonalInfo;
