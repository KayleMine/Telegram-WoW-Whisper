local p = CreateFrame("Frame", nil, UIParent)	
p:SetWidth(1)
p:SetHeight(1)
p:SetPoint("TOPLEFT", 0, 0)
p.Texture = p:CreateTexture()
p.Texture:SetAllPoints() 
local waitTable = {};
local waitFrame = nil;
function WaitMe_wait(delay, func, ...)
  if(type(delay)~="number" or type(func)~="function") then
    return false;
  end
  if(waitFrame == nil) then
    waitFrame = CreateFrame("Frame","WaitFrame", UIParent);
    waitFrame:SetScript("onUpdate",function (self,elapse)
      local count = #waitTable;
      local i = 1;
      while(i<=count) do
        local waitRecord = tremove(waitTable,i);
        local d = tremove(waitRecord,1);
        local f = tremove(waitRecord,1);
        local p = tremove(waitRecord,1);
        if(d>elapse) then
          tinsert(waitTable,i,{d-elapse,f,p});
          i = i + 1;
        else
          count = count - 1;
          f(unpack(p));
        end
      end
    end);
  end
  tinsert(waitTable,{delay,func,{...}});
  return true;
end
 function WaitMe_changePX()
p.Texture:SetColorTexture(0, 0, 5, 1)	
 end	
local WhisperAlertFrame = CreateFrame("Frame")       
WhisperAlertFrame:RegisterEvent("CHAT_MSG_WHISPER")
local function OnEvent(self, event, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, sender, ...)   
local x = 256
	if event == "CHAT_MSG_WHISPER" and (arg6 == "GM" or arg6 == "DEV") then
		x = 0  
		p.Texture:SetTexture(5, 5, 0, 100) -- Set FFFF00 for 5.5 sec when GM\DEV DM received.
		WaitMe_wait(5.5,WaitMe_changePX);	
	elseif event == "CHAT_MSG_WHISPER" and not (arg6 == "GM" or arg6 == "DEV") then
		x = 0  
		p.Texture:SetTexture(2, 0, 0, 100) -- Set FF0000 for 2.5 sec when DM received.
		WaitMe_wait(2.5,WaitMe_changePX);	
	end
end    
WhisperAlertFrame:SetScript("OnEvent", OnEvent)
