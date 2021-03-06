﻿using System;
using System.IO;
using Silanis.ESL.SDK;
using System.Collections.Generic;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class DisabledSignaturesExample : SDKSample
    {
        public static void Main(string [] args)
        {
            new DisabledSignaturesExample().Run();
        }

        private string signer1Id = "signer1Id";
        private string signer2Id = "signer2Id";
        private string documentId = "documentId";
        private string DOCUMENT_NAME = "First Document";

        public IList<Signature> signer1SignableSignatures, signer2SignableSignatures;

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                        .WithFirstName("John1")
                        .WithLastName("Smith1")
                        .WithCustomId(signer1Id))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                        .WithFirstName("John2")
                        .WithLastName("Smith2")
                        .WithCustomId(signer2Id))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                        .FromStream(fileStream1, DocumentType.PDF)
                        .WithId(documentId)
                        .WithSignature(SignatureBuilder.SignatureFor(email1)
                              .OnPage(0)
                              .AtPosition(100, 100))
                        .WithSignature(SignatureBuilder.SignatureFor(email1)
                              .OnPage(0)
                              .AtPosition(150, 100))
                        .WithSignature(SignatureBuilder.SignatureFor(email2)
                              .OnPage(0)
                              .AtPosition(250, 100)
                              .Disabled()))
                    .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.SendPackage(packageId);
            retrievedPackage = eslClient.GetPackage(packageId);

            signer1SignableSignatures = eslClient.ApprovalService.GetAllSignableSignatures(retrievedPackage, documentId, signer1Id);
            signer2SignableSignatures = eslClient.ApprovalService.GetAllSignableSignatures(retrievedPackage, documentId, signer2Id);
        }
    }
}
