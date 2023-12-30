'use client'

import QRCode from "react-qr-code"
import { useRouter } from 'next/navigation'
import { useCurrentUser } from "@/hooks/useCurrentUser"
import { get } from "http"
import { useEffect, useState } from "react"

export default function SignIn() {
    const router = useRouter()
    const { setCurrentUser } = useCurrentUser();
    const [authRequest, setAuthRequest] = useState("");
    const [challenge, setChallenge] = useState("");

    useEffect(() => {
        getAuthRequest();
    }, []);

    function getAuthRequest() {
        fetch("http://localhost:5196/api/get-auth-request", { cache: 'no-store' })
            .then(res => {
                if(res.ok) {
                    res.json().then(resObj => {
                        setChallenge(resObj.challenge);
                        setAuthRequest(JSON.stringify(resObj));
                    });
                }
            });
    }

    function checkAuthStatus(challenge:string) {
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

    return (
        <div className="w-full p-6 m-auto rounded-md shadow-md lg:max-w-lg">
            <h1 className="text-3xl font-semibold text-center text-purple-700">DID Auth Demo</h1>
            <p className="text-center">Please scan the QR Code to with your mobile app to login.</p>
            {authRequest !== "" && <QRCode
                size={256}
                style={{ height: "500", maxWidth: "100%", width: "100%" }}
                value={authRequest}
                viewBox={`0 0 256 256`}
                />}
            <button className='btn btn-primary mt-3' onClick={() => checkAuthStatus(challenge)}>Check Auth Status</button>
        </div>
    )
}