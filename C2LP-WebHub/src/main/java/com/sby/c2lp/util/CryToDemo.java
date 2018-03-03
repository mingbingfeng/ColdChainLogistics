package com.sby.c2lp.util;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.env.Environment;
import org.springframework.security.crypto.encrypt.Encryptors;
import org.springframework.security.crypto.encrypt.TextEncryptor;
import org.springframework.stereotype.Component;

/**
 * Created by xu on 2015/4/20.
 */
@Component
public class CryToDemo {

    private final Logger logger = LoggerFactory.getLogger(CryToDemo.class);

    @Autowired
    private Environment env;

    public String textToEncrypt(String text) {
        final String password = env.getProperty("cryto.password");//"I AM SHERLOCKED";
        final String salt = env.getProperty("cryto.salt");//"bf178bd229544206";// KeyGenerators.string().generateKey();
        TextEncryptor encryptor = Encryptors.text(password, salt);
        logger.debug("Original text: \"" + text + "\"");

        String encryptedText = encryptor.encrypt(text);
        logger.debug("Encrypted text: \"" + encryptedText + "\"");

        return encryptedText;
    }

    public String decrypt(String encryptedText) {
        final String password = env.getProperty("cryto.password");//"I AM SHERLOCKED";
        final String salt = env.getProperty("cryto.salt");//"bf178bd229544206";// KeyGenerators.string().generateKey();
        // Could reuse encryptor but wanted to show reconstructing TextEncryptor
        TextEncryptor decryptor = Encryptors.text(password, salt);
        String decryptedText = decryptor.decrypt(encryptedText);
        logger.debug("Decrypted text: \"" + decryptedText + "\"");

        return decryptedText;

    }


    public static void main(String[] args) {
//        final String password = "I AM SHERLOCKED";
//        final String salt = KeyGenerators.string().generateKey();
//
//        TextEncryptor encryptor = Encryptors.text(password, salt);
//        logger.info("Salt: \"" + salt + "\"");
//
//        String textToEncrypt = "*royal secrets*";
//        logger.info("Original text: \"" + textToEncrypt + "\"");
//
//        String encryptedText = encryptor.encrypt(textToEncrypt);
//        logger.info("Encrypted text: \"" + encryptedText + "\"");
//
//        // Could reuse encryptor but wanted to show reconstructing TextEncryptor
//        TextEncryptor decryptor = Encryptors.text(password, salt);
//        String decryptedText = decryptor.decrypt(encryptedText);
//        logger.info("Decrypted text: \"" + decryptedText + "\"");

//        if(textToEncrypt.equals(decryptedText)) {
//            logger.info("Success: decrypted text matches");
//        } else {
//            logger.info("Failed: decrypted text does not match");
//        }
        CryToDemo demo =  new CryToDemo();
        //demo.decrypt(demo.textToEncrypt("zhaoyou:xxxx:123456789"));
        demo.decrypt("2f0f4311766abca8664cc9e6162c183589c36d29973527667282062e2fabeab4f988533583f9c806609761c1891d54a2");
    }
}
