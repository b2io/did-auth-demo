

import AuthGuard from '@/components/authGuard';
import CredentialDetailSummary from '@/components/credentialDetailSummary';
import React from 'react';

type CredentialDetailPageProps = {
    params: {
        id: string
    }
}

export default function CredentialDetailPage({ params }: CredentialDetailPageProps) {
    return(
        <AuthGuard>
            <h1>Credential Detail</h1>
            <CredentialDetailSummary id={params.id} />
        </AuthGuard>
    )
}