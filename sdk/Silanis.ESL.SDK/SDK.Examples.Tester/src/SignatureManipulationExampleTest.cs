using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using System.Collections.Generic;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignatureManipulationExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SignatureManipulationExample example = new SignatureManipulationExample(Props.GetInstance());
            example.Run();

            // Test if all signatures are added properly
            Dictionary<string,Signature> signaturesDictionary = ConvertListToMap(example.addedSignatures);

            Assert.IsTrue(signaturesDictionary.ContainsKey(example.email1));
            Assert.IsTrue(signaturesDictionary.ContainsKey(example.email2));
            Assert.IsTrue(signaturesDictionary.ContainsKey(example.email3));

            // Test if signature1 is deleted properly
            signaturesDictionary = ConvertListToMap(example.deletedSignatures);

            Assert.IsFalse(signaturesDictionary.ContainsKey(example.email1));
            Assert.IsTrue(signaturesDictionary.ContainsKey(example.email2));
            Assert.IsTrue(signaturesDictionary.ContainsKey(example.email3));

            // Test if signature3 is updated properly and is assigned to signer1
            signaturesDictionary = ConvertListToMap(example.updatedSignatures);

            Assert.IsTrue(signaturesDictionary.ContainsKey(example.email1));
            Assert.IsTrue(signaturesDictionary.ContainsKey(example.email2));
            Assert.IsFalse(signaturesDictionary.ContainsKey(example.email3));

        }

        private Dictionary<string,Signature> ConvertListToMap(List<Signature> signaturesList)
        {
            Dictionary<string,Signature> signaturesDictionary = new Dictionary<string,Signature>();
            foreach(Signature signature in signaturesList)
            {
                signaturesDictionary.Add(signature.SignerEmail, signature);
            }
            return signaturesDictionary;
        }

    }
}

