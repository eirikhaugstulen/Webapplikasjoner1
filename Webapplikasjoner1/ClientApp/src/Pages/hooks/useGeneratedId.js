import React, {useCallback, useEffect, useState} from "react";
import {generateStringId} from "../../utils/generateStringId";

export const useGeneratedId = () => {
    const initId = generateStringId();
    const [generatedId, setGeneratedId] = useState(initId);
    
    const refetchId = () => setGeneratedId(generateStringId());
    
    return {
        generatedId,
        refetchId,
    }
}