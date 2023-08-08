import AuthGuard from "@/components/authGuard";
import PageTitle from "@/components/pageTitle";
import CreateCredentialForm from "./createCredentialForm";
import { CredentialDetail } from "@/models";

export default function CreateCredentialPage() {

    const newCredentialDetail: CredentialDetail = {
        id: 0,
        name: '',
        description: '',
        ownerDid: '',
        schemaDefinition: [],
        createdAt: new Date(),
        updatedAt: new Date()
    };

    return (
        <AuthGuard>
            <CreateCredentialForm credentialDetail={newCredentialDetail} />
        </AuthGuard>
    )
}