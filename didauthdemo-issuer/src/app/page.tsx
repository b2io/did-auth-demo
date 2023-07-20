import React from 'react';
import QRCode from "react-qr-code";

import CheckStatus from './checkStatus';

let challenge = "";

async function getAuthRequest() {
  const res = await fetch("http://localhost:5196/api/get-auth-request", { cache: 'no-store' });
  
  // Recommendation: handle errors
  if (!res.ok) {
    // This will activate the closest `error.js` Error Boundary
    throw new Error('Failed to fetch data')
  }

  const request = await res.json();
  challenge = request.challenge;

  return JSON.stringify(request);
}

export default async function Home() {
  const authRequest = await getAuthRequest();
  console.log("payload", authRequest);

  return (
    <div className="w-full p-6 m-auto rounded-md shadow-md lg:max-w-lg">
        <h1 className="text-3xl font-semibold text-center text-purple-700">DID Auth Demo</h1>
        <p className="text-center">Please scan the QR Code to with your mobile app to login.</p>
        <QRCode
          size={256}
          style={{ height: "500", maxWidth: "100%", width: "100%" }}
          value={authRequest}
          viewBox={`0 0 256 256`}
          />
          <CheckStatus challenge={challenge} />
    </div>
  )
}
