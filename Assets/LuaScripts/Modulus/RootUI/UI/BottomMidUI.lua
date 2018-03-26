BottomMidUI = SimpleClass(BaseUI)

local mainId = 900000010001 
--声明成员变量
function BottomMidUI:__init_Self()
	self.mp = UIWidget.LImage
	self.hp = UIWidget.LImage
	self.createPlayerBtn = UIWidget.LButton
	self.deleteBtn = UIWidget.LButton
	self.createAIBtn = UIWidget.LButton
	self.index = 800000010001
	self.entityIndex = 10001
end 

function BottomMidUI:initLayout()   
    

    LuaExtend:doFloatTo(function(val) self.hp:setMaterialFloat("_Fill",val) end,1,0.65,0.5)
    LuaExtend:doFloatTo(function(val) self.mp:setMaterialFloat("_Fill",val) end,1,0.25,0.5)

    self.createPlayerBtn:setOnClick(function() 
        --print("卸载所有AB")
        --LuaExtend:unloadAllAssetBundle()
	    local conf = ConfigHelper:getConfigByKey('EntityConfig',10001)
	    local roleData = EntityData(mainId,conf)
	    EntityMgr:createEntity(roleData)
    end)
    self.deleteBtn:setOnClick(function() 
    	--EntityMgr:destroyEntity(mainId)
        local bdata = BulletHelper:getData(0)
        local x = math.random(-100,100)
        local z = math.random(-100,100)
        bdata.startPos = Vector3(x,10,z)
        bdata.goalUID = mainId
        bdata.callBack = function(bid,eid,pos)
           --print(bid)
           --print(eid)
           --print(pos)
           local effPath = "effectprefabs/31_RFX_Magic_Pulse1"
           LuaExtend:loadObj(effPath,function(obj)  
              LuaExtend:setObjPos(obj,pos.x,pos.y,pos.z)
           end)
        end
        LuaExtend:createBullet(bdata)
    end)

    self.createAIBtn:setOnClick(function() 
    	self.index = self.index + 1
    	self.entityIndex = self.entityIndex + 1 
    	if self.entityIndex >10003 then 
    		self.entityIndex = 10002
    	end 
	    local conf = ConfigHelper:getConfigByKey('EntityConfig',self.entityIndex)
	    local roleData = EntityData(self.index,conf)
	    EntityMgr:createEntity(roleData)
    end)
end 

function BottomMidUI:onOpen()

end 