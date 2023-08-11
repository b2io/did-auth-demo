'use client'

import Modal from "@/components/modal";
import SectionTitle from "@/components/sectionTitle";
import { CredentialDetail } from "@/models";
import { useState } from "react";
import QRCode from "react-qr-code";

type CredentialRequestSectionProps = {
    credential: CredentialDetail
}

export default function CredentialRequestSection({credential}: CredentialRequestSectionProps) {

    function getAuthRequest() {
        return JSON.stringify({
            type: "CredentialRequest",
            credentialId: credential.id,
            ownerDid: credential.ownerDid,
            name: credential.name,
            description: credential.description,
            schemaDefinition: credential.schemaDefinition
        })
    }

    return (
        <div>
            <div className="grid grid-cols-2 gap-2 mb-3">
                <SectionTitle title='Credential Requests' />
                <div>
                    <button className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4  font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none float-right" onClick={() => window.my_modal_1.showModal()}>Show Invite</button>
                </div>
            </div>
            <div>
                <dialog id="my_modal_1" className="modal">
                    <form method="dialog" className="modal-box">
                        <p className="text-lg font-bold leading-none tracking-tight mb-3">Please scan with Identity Wallet</p>
                        <QRCode
                            className="m-auto"
                            // size={256}
                            // style={{ height: "500", maxWidth: "100%", width: "100%" }}
                            value={getAuthRequest()}
                            //viewBox={`0 0 256 256`}
                            />
                        {/* if there is a button in form, it will close the modal */}
                        <button className="btn mt-3">Close</button>
                    </form>
                </dialog>
            </div>
        </div>
    )
}