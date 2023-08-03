'use client'

import QRCode from "react-qr-code"
import { useRouter } from 'next/navigation'

/////////////////////////
//i dont like this being in this file
export type SessionCheckProps = {
    authRequest: any,
    challenge: string,
    setCurrentUser: any
}

function checkAuthStatus(challenge:string, setCurrentUser: any) {
    const router = useRouter()
    fetch(`http://localhost:5196/api/check-auth-status/${challenge}`, { cache: 'no-store'})
        .then(res => {
            if(res.ok) {
                res.json().then(resObj => {
                    if(resObj.isAuthenticated) {
                        setCurrentUser({accessToken: resObj.accessToken})
                        router.replace('/');
                    }else {
                        alert("Not Authenticated")
                    }
                });
            }
        });
}

export default function SignIn({authRequest, challenge, setCurrentUser}: SessionCheckProps) {
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
            <button className='btn btn-primary mt-3' onClick={async () => await checkAuthStatus(challenge, setCurrentUser)}>Check Auth Status</button>
        </div>
    )
}