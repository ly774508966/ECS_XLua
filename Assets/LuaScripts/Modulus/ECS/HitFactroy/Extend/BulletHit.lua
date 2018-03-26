BulletHit = SimpleClass(BaseHit)

function BulletHit:__init(...)

end 

function BulletHit:onExecute() 
	local bulletConf = ConfigHelper:getConfigByKey('BulletConfig',self.cfg.mapId)
    local bdata = BulletHelper:getData(self.cfg.mapId)
    local player = EntityMgr:getEntity(self.roleId)
    if player then 
	    bdata.startPos = player.root.transform.position
	    bdata.goalUID = 800000010002
	    bdata.callBack = function(bid,eid,pos)
	        LuaExtend:loadObj(bdata.expPath,function(obj)  
	          LuaExtend:setObjPos(obj,pos.x,pos.y,pos.z)
	        end)
	        local audioConf = ConfigHelper:getConfigByKey('AudioConfig',bulletConf.expAudio)
	        if audioConf then 
	        	LuaExtend:playAtPoint(audioConf.path,pos)
	        end
	    end
	    local audioConf = ConfigHelper:getConfigByKey('AudioConfig',bulletConf.startAudio)
	    if audioConf then 
	        LuaExtend:playAtPoint(audioConf.path,bdata.startPos)
	    end
	    LuaExtend:createBullet(bdata)
    end
end 